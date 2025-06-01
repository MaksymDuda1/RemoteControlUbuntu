using RemoteControlUbuntu.Application.Abstractions.Repositories;

namespace RemoteControlUbuntu.Infrastructure.Abstractions.Repositories;

public interface IUnitOfWork
{
    IConnectionRepository Connections { get; }

    ICommandRepository Commands { get; }

    IUserRepository Users { get; }

    Task SaveAsync();
}