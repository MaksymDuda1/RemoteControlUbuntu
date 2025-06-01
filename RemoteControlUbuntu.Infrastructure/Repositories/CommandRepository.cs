using RemoteControlUbuntu.Application.Abstractions.Repositories;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class CommandRepository(RemoteDbContext context) 
    : BaseRepository<Command>(context), ICommandRepository
{
    
}