using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class CommandsBlackListRepository(RemoteDbContext context) : BaseRepository<CommandsBlackList>(context), ICommandsBlackListRepository
{
    
}