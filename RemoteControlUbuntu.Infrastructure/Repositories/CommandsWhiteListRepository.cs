using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class CommandsWhiteListRepository(RemoteDbContext context) : BaseRepository<CommandsWhiteList>(context), ICommandsWhiteListRepository
{
    
}