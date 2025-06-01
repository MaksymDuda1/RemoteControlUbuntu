using RemoteControlUbuntu.Application.Abstractions.Repositories;
using RemoteControlUbuntu.Infrastructure.Abstractions.Repositories;
using IConnectionRepository = RemoteControlUbuntu.Infrastructure.Abstractions.Repositories.IConnectionRepository;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class UnitOfWork(
    RemoteDbContext context,
    Lazy<IUserRepository> userRepository,
    Lazy<IConnectionRepository> connectionRepository,
    Lazy<ICommandRepository> commandRepository) : IUnitOfWork
{
    public IUserRepository Users => userRepository.Value;
    public IConnectionRepository Connections => connectionRepository.Value;

    public ICommandRepository Commands => commandRepository.Value;
    
    public async Task SaveAsync() => await context.SaveChangesAsync();
}

