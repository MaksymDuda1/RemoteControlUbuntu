using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class UserCommandsWhiteListRepository(RemoteDbContext context) : BaseRepository<UserCommandsWhiteList>(context), IUserCommandsWhiteListRepository
{
    
}