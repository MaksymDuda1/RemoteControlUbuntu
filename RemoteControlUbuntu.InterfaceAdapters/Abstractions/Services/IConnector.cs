using RemoteControlUbuntu.Domain.Entities;
using Renci.SshNet;

namespace RemoteControlUbuntu.Infrastructure.Abstractions.Services;

public interface IConnector
{
    SshClient GetConnection(Connection connection);
}