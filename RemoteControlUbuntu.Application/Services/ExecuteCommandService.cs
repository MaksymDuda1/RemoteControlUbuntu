using AutoMapper;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Connectors;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;
using Renci.SshNet.Common;

namespace RemoteControlUbuntu.Application.Services;

public class ExecuteCommandService(
    IConnector connector,
    IConnectionService connectionService,
    ICommandService commandService,
    IMapper mapper) : IExecuteCommandService
{
    public async Task Execute(UserConnectionCommandDto request)
    {
        var connection = mapper.Map<Connection>(await connectionService.GetConnectionById(request.ConnectionId));
        var command = mapper.Map<Command>(await commandService.GetCommandById(request.CommandId));
        var client = connector.GetConnection(connection);
      
        Console.WriteLine(client.RunCommand($"export DISPLAY=localhost:10.0;" +
                                            "export XAUTHORITY=$HOME/.Xauthority;" +
                                            $"{command.TerminalCommand}").Error);
        client.Disconnect();
        Console.WriteLine("Disconnected");
    }
}