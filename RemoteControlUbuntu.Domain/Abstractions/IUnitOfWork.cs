using System.Windows.Input;

namespace RemoteControlUbuntu.Domain.Abstractions;

public interface IUnitOfWork
{
    IConnectionRepository Connections { get; }
    
    ICommandRepository Commands { get; }
    
    IUserRepository Users { get; }
    
    Task SaveAsync();
}