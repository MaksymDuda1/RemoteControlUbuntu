using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.Services;

public interface ICommandSetService
{
    Task<List<CommandSetDto>> GetCommandSetsAsync(Guid userId);
    Task<CommandSetDto> GetCommandSetByIdAsync(Guid id);
    Task CreateCommandSetAsync(CommandSet commandSet);
    Task UpdateCommandSetAsync(CommandSet commandSet);
    Task DeleteCommandSetAsync(Guid commandSetId);
}