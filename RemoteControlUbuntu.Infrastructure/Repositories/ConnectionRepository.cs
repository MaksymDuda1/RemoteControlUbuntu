using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class ConnectionRepository(RemoteDbContext context)
    : BaseRepository<Connection>(context), IConnectionRepository
{
    
}