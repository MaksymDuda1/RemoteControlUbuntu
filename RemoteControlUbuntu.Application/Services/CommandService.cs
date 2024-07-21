using AutoMapper;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Exceptions;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.Services;

public class CommandService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandService
{
    public async Task<List<CommandDto>> GetAllUserCommand(Guid userId)
    {
        var commands = await unitOfWork.Commands
            .GetByConditionsAsync(c => c.UserId == userId);

        return commands.Select(mapper.Map<CommandDto>).ToList();
    }

    public async Task<CommandDto> GetCommandById(Guid commandId)
    {
        var command = await unitOfWork.Commands
            .GetSingleByConditionAsync(c => c.Id == commandId);

        if (command == null)
            throw new EntityNotFoundException("command Not Found");

        return mapper.Map<CommandDto>(command);
    }

    public async Task AddCommand(AddCommandDto commandDto)
    {
        var user = await unitOfWork.Users
            .GetSingleByConditionAsync(u => u.Id == commandDto.UserId);

        if (user == null)
            throw new EntityNotFoundException("User Not Found");

        var command = mapper.Map<Command>(commandDto);

        await unitOfWork.Commands.InsertAsync(command);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateCommand(UpdateCommandDto updateCommandDto)
    {
        var command = await unitOfWork.Commands
            .GetSingleByConditionAsync(c => c.Id == updateCommandDto.Id);

        if (command == null)
            throw new EntityNotFoundException("command Not Found");

        command.TerminalCommand = updateCommandDto.TerminalCommand;

        unitOfWork.Commands.Update(command);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteCommand(Guid commandId)
    {
        await unitOfWork.Commands.Delete(commandId);
        await unitOfWork.SaveAsync();
    }
}