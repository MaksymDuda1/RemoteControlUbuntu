using RemoteControlUbuntu.Domain.DTOs;

namespace RemoteControlUbuntu.Application.Abstractions.Services;

public interface IExecuteCommandService
{
    Task Execute(ConnectionCommandDto request);
}