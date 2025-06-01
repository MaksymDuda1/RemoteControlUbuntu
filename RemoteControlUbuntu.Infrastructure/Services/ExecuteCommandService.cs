using AutoMapper;
using RemoteControlUbuntu.Application.Abstractions.Services;
using RemoteControlUbuntu.Domain.DTOs;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Infrastructure.Abstractions.Services;
using IConnector = RemoteControlUbuntu.Infrastructure.Abstractions.Services.IConnector;

namespace RemoteControlUbuntu.Infrastructure.Services;

public class ExecuteCommandService(
    IConnector connector,
    IConnectionService connectionService,
    ICommandService commandService,
    IMapper mapper) : IExecuteCommandService
{
    public async Task Execute(ConnectionCommandDto request)
    {
        var connection = mapper.Map<Connection>(await connectionService.GetConnectionById(request.ConnectionId));
        var command = mapper.Map<Command>(await commandService.GetCommandById(request.CommandId));
        var client = connector.GetConnection(connection);
      
        Console.WriteLine(client.RunCommand($"export DISPLAY=:1;" +
                                            "export XAUTHORITY=$HOME/.Xauthority;" +
                                            $"{command.TerminalCommand}").Error);
        client.Disconnect();
        Console.WriteLine("Disconnected");
    }
}