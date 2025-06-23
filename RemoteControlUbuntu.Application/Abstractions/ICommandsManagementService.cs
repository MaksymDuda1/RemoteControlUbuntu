using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface ICommandsManagementService
{
    Task<List<BlackListCommandDto>> GetBlackListCommands();
    Task AddCommandToBlackList(AddCommandToBlackListDto dto);
    Task UpdateBlackListCommand(Guid id, BlackListCommandDto dto);
    Task DeleteBlackListCommand(Guid id);
    Task<List<WhiteListCommandDto>> GetWhiteListCommands();
    Task AddCommandToWhiteList(AddCommandToBlackListDto dto);
    Task UpdateWhiteListCommands(Guid id, WhiteListCommandDto dto);
    Task DeleteWhiteListCommand(Guid id);
}