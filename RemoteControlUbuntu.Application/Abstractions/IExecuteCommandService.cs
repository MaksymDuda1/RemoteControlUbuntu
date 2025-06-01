using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface IExecuteCommandService
{
    Task<CommandExecutionResult> Execute(ConnectionCommandDto request);
}