using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface ICommandService
{
    Task<List<CommandDto>> GetAllUserCommand(Guid userId);
    Task<CommandDto> GetCommandById(Guid commandId);
    Task AddCommand(AddCommandDto commandDto);
    Task UpdateCommand(UpdateCommandDto updateCommandDto);
    Task DeleteCommand(Guid commandId);
}