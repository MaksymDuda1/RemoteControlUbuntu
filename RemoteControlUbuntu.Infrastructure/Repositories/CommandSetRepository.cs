using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class CommandSetRepository(RemoteDbContext context) : BaseRepository<CommandSet>(context), ICommandSetRepository
{
    
}