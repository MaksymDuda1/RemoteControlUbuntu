using RemoteControlUbuntu.Domain.Entities;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace RemoteControlUbuntu.Application.Connectors;

public class Connector() : IConnector
{
    public SshClient GetConnection(Connection connection)
    {
        try
        {
            var client = new SshClient(
                connection.Host,
                connection.Username,
                connection.Password);
            client.Connect();
            if (client.IsConnected)
            {
                return client;
            }

            throw new SshConnectionException("Can not connect to your ssh client");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}