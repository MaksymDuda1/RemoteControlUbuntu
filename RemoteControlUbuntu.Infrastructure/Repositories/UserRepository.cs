using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class UserRepository(RemoteDbContext context)
    :BaseRepository<User>(context), IUserRepository
{
    
}