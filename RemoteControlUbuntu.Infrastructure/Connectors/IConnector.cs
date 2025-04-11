using RemoteControlUbuntu.Domain.Entities;
using Renci.SshNet;

namespace RemoteControlUbuntu.Infrastructure.Connectors;

public interface IConnector
{
    SshClient GetConnection(Connection connection);
}