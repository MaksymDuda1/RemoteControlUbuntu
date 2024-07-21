using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface IExecuteCommandService
{
    Task Execute(UserConnectionCommandDto request);
}