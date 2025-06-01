using RemoteControlUbuntu.Domain.Abstractions;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class UnitOfWork(
    RemoteDbContext context,
    Lazy<IUserRepository> userRepository,
    Lazy<IConnectionRepository> connectionRepository,
    Lazy<ICommandRepository> commandRepository,
    Lazy<IUserCommandsWhiteListRepository> userCommandsListRepository,
    Lazy<ICommandsBlackListRepository> commandsBlackListRepository,
    Lazy<ICommandsWhiteListRepository> commandsWhiteListRepository) : IUnitOfWork
{
    public IUserRepository Users => userRepository.Value;
    public IConnectionRepository Connections => connectionRepository.Value;

    public ICommandRepository Commands => commandRepository.Value;
    
    public ICommandsBlackListRepository CommandsBlackList => commandsBlackListRepository.Value;
    
    public ICommandsWhiteListRepository CommandsWhiteList => commandsWhiteListRepository.Value;
    
    public IUserCommandsWhiteListRepository UserCommandsWhiteList => userCommandsListRepository.Value;
    
    public async Task SaveAsync() => await context.SaveChangesAsync();
}

