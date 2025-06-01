using AutoMapper;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Infrastructure.Connectors;
using Renci.SshNet;

namespace RemoteControlUbuntu.Application.Services;

public class ExecuteCommandService(
    IConnector connector,
    IConnectionService connectionService,
    ICommandService commandService,
    IMapper mapper,
    IValidationService validationService) : IExecuteCommandService
{
    public async Task<CommandExecutionResult> Execute(ConnectionCommandDto request)
    {
        var connection = mapper.Map<Connection>(await connectionService.GetConnectionById(request.ConnectionId));
        var command = mapper.Map<Command>(await commandService.GetCommandById(request.CommandId));

        var client = connector.GetConnection(connection);

        try
        {
            var preCheckCommand = await validationService.GetCommandToCheckExecutionPossibility(command.Type);
            if (!string.IsNullOrWhiteSpace(preCheckCommand))
            {
                var checkResult = client.RunCommand(preCheckCommand);
                var isExecutable = await validationService.GetExecutionPossibility(checkResult.Result, command.TerminalCommand);

                if (!isExecutable)
                {
                    return new CommandExecutionResult
                    {
                        Success = false,
                        Error = "The command could not be executed in your environment.",
                        ExitStatus = 404
                    };
                }
            }

            var fullCommand = $"export DISPLAY=:1;" +
                                    "export XAUTHORITY=$HOME/.Xauthority;" +
                                    $"{command.TerminalCommand}";

            var executionResult = client.RunCommand(fullCommand);

            return new CommandExecutionResult
            {
                Success = executionResult.ExitStatus == 0,
                Output = executionResult.Result,
                Error = executionResult.Error,
                ExitStatus = executionResult.ExitStatus
            };
        }
        finally
        {
            client.Disconnect();
        }
    }
}