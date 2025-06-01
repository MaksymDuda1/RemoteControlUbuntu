using System.ComponentModel.DataAnnotations;
using FluentResults;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Constants;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Domain.Enums;
using IMapper = AutoMapper.IMapper;

namespace RemoteControlUbuntu.Application.Services;

public interface IValidationService
{
    Task<Result<string?>> GetCommandFromUserRequestAndCheckBlackListMatches(string request);
    Task<ValidationResultDto> ValidateCommandAndCheckOnAbsenceOfDangerousCode(Guid userId, AddCommandDto addCommandDto);

    Task<string?> GetCommandToCheckExecutionPossibility(string type);

    Task<bool> GetExecutionPossibility(string objects, string command);
}

public class ValidationService(IUnitOfWork unitOfWork, IMapper mapper, IOpenAIService openAIService) : IValidationService
{
    public async Task<Result<string?>> GetCommandFromUserRequestAndCheckBlackListMatches(string request)
    {
        var getCommandPrompt = $@""" Below is a user request for a Linux terminal command.  
                        You should return the terminal command in the following JSON format:

                        {{
                          ""general_command_name"": ""Short description of the command"",
                          ""terminal_command"": ""Actual terminal command""
                        }}

                        Here is the user request:  
                        {request}
                        """;
        
        var commandResponse =  await openAIService.AskChatGPT(getCommandPrompt);

        var blackList = await unitOfWork.CommandsBlackList.GetAllAsync();

        var blackListCheckPrompt = $@""" Your task is to validate the command provided below.
                                    Command to validate:
                                    {commandResponse}
                                    
                                    List of forbidden commands:
                                    {blackList}
                                    
                                    Instructions:
                                    1. Check if the `general_command_name` or the `terminal_command` from `commandResponse` matches or relates to any item in the `blackList`.
                                    2. If it is related to a forbidden command, return: `Command is not allowed`
                                    3. If the command is safe, return: true
                                    4. Write just response, without any additional text
                                   """;
        
        var validationResponse = await openAIService.AskChatGPT(blackListCheckPrompt);
        
        return bool.Parse(validationResponse) ? Result.Ok(commandResponse) : Result.Fail(blackListCheckPrompt);
    }
    
    public async Task<ValidationResultDto> ValidateCommandAndCheckOnAbsenceOfDangerousCode(Guid userId, AddCommandDto addCommandDto)
    {
        var blackList = await unitOfWork.CommandsBlackList.GetAllAsync();

        var blackListCheckPrompt = $@""" Your task is to validate the command provided below.
                                    Command to validate:
                                    {addCommandDto.TerminalCommand}
                                    
                                    List of forbidden commands:
                                    {blackList}
                                    
                                    Instructions:
                                    1. Check if command  matches or relates to any item in the `blackList`.
                                    2. If it is related to a forbidden command, return: `BlackListedCommand`
                                    3. Check if command contains any dangerous code, if yes return: `BlackListedCommand`
                                    4. Write just response, without any additional text
                                   """;
        
        var blackListValidationResponse = await openAIService.AskChatGPT(blackListCheckPrompt);

        Enum.TryParse<CommandValidationResult>(blackListValidationResponse, out var blackListValidationResult);
        
        if (CommandValidationResult.BlackListed == blackListValidationResult)
        {
            return new ValidationResultDto()
            {
                CommandValidationResult = CommandValidationResult.BlackListed,
                Message = CommandValidationMessageConstant.BlackListedCommandMessage
            };
        }
        
        var whiteList = await unitOfWork.CommandsWhiteList.GetAllAsync();
        var user = await unitOfWork.Users.GetSingleByConditionAsync(x => x.Id == userId, x => x.UserWhiteList);

        var combinedWhiteList = whiteList.Select(x => new CombinedWhiteListModel()
        {
            Name = x.Name,
            Commands = x.Commands
        }).Concat(user.UserWhiteList.Select(x => new CombinedWhiteListModel()
        {
            Name = x.Name,
            Commands = [x.Command]
        }));
        
        var whiteListCheckPrompt = $@""" Your task is to check relation the command provided below.
                                    Command to validate:
                                    {addCommandDto.TerminalCommand}
                                    
                                    White list:
                                    {combinedWhiteList}
                                    
                                    Instructions:
                                    1. Check if command  matches or relates to any item in the `whiteList`.
                                    2. If it is related to a reliable commands, return: `WhiteListedCommand`
                                    3. Write just response, without any additional text
                                   """;
        
        var whiteListValidationResponse = await openAIService.AskChatGPT(whiteListCheckPrompt);
        
        Enum.TryParse<CommandValidationResult>(whiteListValidationResponse, out var whiteListValidationResult);

        if (CommandValidationResult.WhiteListed == whiteListValidationResult)
        {
            return new ValidationResultDto()
            {
                CommandValidationResult = CommandValidationResult.WhiteListed,
                Message = CommandValidationMessageConstant.WhiteListedCommandMessage
            };
        }
        
        user.UserWhiteList.Add(mapper.Map<UserCommandsWhiteList>(addCommandDto));
        await unitOfWork.SaveAsync();

        return new ValidationResultDto()
        {
            CommandValidationResult = CommandValidationResult.Custom,
            Message = CommandValidationMessageConstant.CustomCommandMessage
        };
    }

        public async Task<string?> GetCommandToCheckExecutionPossibility(string userCommand)
        {
            var prompt = $@"""Your task is to return a Linux terminal command that checks for all related objects (e.g., folders or programs) 
                                to ensure that the next user command can be executed.

                                Type:
                                {userCommand}

                                Instructions:
                                1. If this check is possible, return only the Linux terminal command.
                                2. If this check is not possible, return absolutely no output at all — not even an empty string or placeholder.
                                3. Do not explain or include any additional text — only return the command or nothing.""";    

            var response = await openAIService.AskChatGPT(prompt);

            return string.IsNullOrWhiteSpace(response) ? null : response.Trim();
        }
    
    public async Task<bool> GetExecutionPossibility(string objects, string command)
    {
        var prompt = $@""" Your task is to ensure that command can be executed in this environment:
                                    Environment:
                                    {objects}
                                        
                                    Command:                              
                                    {command}

                                    Instructions:
                                    1. If execution is possible return true
                                    2. If execution is not possible return false
                                    2. Write just response, without any additional text
                                   """;
        
        var executionPossibilityResponse = await openAIService.AskChatGPT(prompt);
        
        return bool.Parse(executionPossibilityResponse!);
    }
}