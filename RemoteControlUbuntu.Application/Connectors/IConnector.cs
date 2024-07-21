using RemoteControlUbuntu.Domain.Entities;
using Renci.SshNet;

namespace RemoteControlUbuntu.Application.Connectors;

public interface IConnector
{
    SshClient GetConnection(Connection connection);
}