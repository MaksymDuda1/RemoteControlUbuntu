using AutoMapper;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Infrastructure.Connectors;
using Renci.SshNet;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using Microsoft.Extensions.Logging;
using Command = RemoteControlUbuntu.Domain.Entities.Command;

namespace RemoteControlUbuntu.Application.Services;

public class ExecuteCommandService(
    IConnector connector,
    IConnectionService connectionService,
    ICommandService commandService,
    IMapper mapper,
    IValidationService validationService,
    ILogger<ExecuteCommandService> logger) : IExecuteCommandService
{
    public async Task<CommandExecutionResult> Execute(ConnectionCommandDto request)
    {
        var connection = mapper.Map<Connection>(await connectionService.GetConnectionById(request.ConnectionId));
        var command = mapper.Map<Command>(await commandService.GetCommandById(request.CommandId));

        // Determine OS type and execute appropriate method
        return connection.Os?.ToLower() switch
        {
            "windows" => await ExecuteWindowsCommand(connection, command),
            "ubuntu" or "linux" => await ExecuteLinuxCommand(connection, command),
            _ => new CommandExecutionResult
            {
                Success = false,
                Error = $"Unsupported OS: {connection.Os}",
                ExitStatus = -1
            }
        };
    }

    private async Task<CommandExecutionResult> ExecuteLinuxCommand(Connection connection, Command command)
    {
        try
        {
            // Try SSH first
            var sshResult = await TrySSHConnection(connection, command);
            if (sshResult.ConnectionEstablished)
                return sshResult.Result;

            // If SSH fails, try reverse SSH tunnel
            logger.LogInformation("Direct SSH failed, attempting reverse tunnel...");
            return await ExecuteViaReverseTunnel(connection, command);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to execute Linux command");
            return new CommandExecutionResult
            {
                Success = false,
                Error = ex.Message,
                ExitStatus = -1
            };
        }
    }

    private async Task<(bool ConnectionEstablished, CommandExecutionResult Result)> TrySSHConnection(
        Connection connection, Command command)
    {
        try
        {
            var client = connector.GetConnection(connection) as SshClient;
            if (client == null)
            {
                return (false, new CommandExecutionResult
                {
                    Success = false,
                    Error = "Failed to create SSH client",
                    ExitStatus = -1
                });
            }

            client.Connect();

            var preCheckCommand = await validationService.GetCommandToCheckExecutionPossibility(command.Type);
            if (!string.IsNullOrWhiteSpace(preCheckCommand))
            {
                var checkResult = client.RunCommand(preCheckCommand);
                var isExecutable = await validationService.GetExecutionPossibility(checkResult.Result, command.TerminalCommand);

                if (!isExecutable)
                {
                    client.Disconnect();
                    return (true, new CommandExecutionResult
                    {
                        Success = false,
                        Error = "The command could not be executed in your environment.",
                        ExitStatus = 404
                    });
                }
            }

            var fullCommand = BuildLinuxCommand(command);
            var executionResult = client.RunCommand(fullCommand);

            client.Disconnect();

            return (true, new CommandExecutionResult
            {
                Success = executionResult.ExitStatus == 0,
                Output = executionResult.Result,
                Error = executionResult.Error,
                ExitStatus = executionResult.ExitStatus
            });
        }
        catch (Exception)
        {
            return (false, null);
        }
    }

    private async Task<CommandExecutionResult> ExecuteViaReverseTunnel(Connection connection, Command command)
    {
        // Implementation for reverse SSH tunnel
        // This allows connection without opening ports on the target machine
        var tunnelService = new ReverseTunnelService();
        
        try
        {
            // Establish reverse tunnel through a relay server
            var tunnel = await tunnelService.EstablishTunnel(connection);
            
            // Execute command through the tunnel
            var result = await tunnel.ExecuteCommand(BuildLinuxCommand(command));
            
            return new CommandExecutionResult
            {
                Success = result.Success,
                Output = result.Output,
                Error = result.Error,
                ExitStatus = result.ExitCode
            };
        }
        catch (Exception ex)
        {
            return new CommandExecutionResult
            {
                Success = false,
                Error = $"Reverse tunnel failed: {ex.Message}",
                ExitStatus = -1
            };
        }
    }

    private string BuildLinuxCommand(Command command)
    {
        // Build command with proper environment setup
        var envSetup = command.Type?.ToLower() switch
        {
            "gui" or "desktop" => "export DISPLAY=:0; export XAUTHORITY=$HOME/.Xauthority; ",
            _ => ""
        };

        return $"{envSetup}{command.TerminalCommand}";
    }

    private async Task<CommandExecutionResult> ExecuteWindowsCommand(Connection connection, Command command)
    {
        try
        {
            /*// Try WinRM first
            var winrmResult = await TryWinRMConnection(connection, command);
            if (winrmResult.ConnectionEstablished)
                return winrmResult.Result;*/

            // If WinRM fails, try PowerShell over SSH (if Windows SSH is enabled)
            logger.LogInformation("WinRM failed, attempting SSH to Windows...");
            var sshResult = await TryWindowsSSH(connection, command);
            if (sshResult.ConnectionEstablished)
                return sshResult.Result;

            // If both fail, try reverse connection
            logger.LogInformation("Direct connections failed, attempting reverse connection...");
            return await ExecuteWindowsViaReverse(connection, command);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to execute Windows command");
            return new CommandExecutionResult
            {
                Success = false,
                Error = ex.Message,
                ExitStatus = -1
            };
        }
    }

    private async Task<(bool ConnectionEstablished, CommandExecutionResult Result)> TryWinRMConnection(Connection connection, Command command)
    {
        try
        {
            var securePassword = new SecureString();
            foreach (char c in connection.Password)
                securePassword.AppendChar(c);

            var credential = new PSCredential(connection.Username, securePassword);
            
            var connectionInfo = new WSManConnectionInfo(
                new Uri($"http://{connection.Host}:5985/wsman"),
                "http://schemas.microsoft.com/powershell/Microsoft.PowerShell",
                credential);

            connectionInfo.AuthenticationMechanism = AuthenticationMechanism.Negotiate;
            connectionInfo.SkipCACheck = true;
            connectionInfo.SkipCNCheck = true;
            connectionInfo.SkipRevocationCheck = true;

            using var runspace = RunspaceFactory.CreateRunspace(connectionInfo);
            runspace.Open();

            using var pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(ConvertToWindowsCommand(command));
            
            var results = pipeline.Invoke();
            var output = string.Join(Environment.NewLine, results.Select(r => r.ToString()));
            var errors = pipeline.Error.ReadToEnd();
            var errorOutput = string.Join(Environment.NewLine, errors.Select(e => e.ToString()));

            runspace.Close();

            return (true, new CommandExecutionResult
            {
                Success = string.IsNullOrEmpty(errorOutput),
                Output = output,
                Error = errorOutput,
                ExitStatus = string.IsNullOrEmpty(errorOutput) ? 0 : 1
            });
        }
        catch (Exception)
        {
            return (false, null);
        }
    }

    private async Task<(bool ConnectionEstablished, CommandExecutionResult Result)> TryWindowsSSH(
        Connection connection, Command command)
    {
        try
        {
            var client = new SshClient(connection.Host, connection.Username, connection.Password);
            await client.ConnectAsync(new CancellationToken());
            
            var taskName = command.Name.Replace(" ", "");
            
            // Створюємо завдання для негайного виконання
            string createTaskCmd = $"schtasks /Create /TN \"{taskName}\" /TR \"cmd /c {command.TerminalCommand}\" /SC ONCE /ST 00:00 /F";
            string runTaskCmd = $"schtasks /Run /TN \"{taskName}\"";
            
            var createResult = client.RunCommand(createTaskCmd);
            
            var result = client.RunCommand(runTaskCmd);
                
            // Чекаємо достатньо часу для виконання
            await Task.Delay(5000);
                
            // Перевіряємо статус завдання
            var statusResult = client.RunCommand($"schtasks /Query /TN \"{taskName}\" /FO LIST");
            Console.WriteLine($"Task Status: {statusResult.Result}");
                
            // Видаляємо завдання після виконання
            client.RunCommand($"schtasks /Delete /TN \"{taskName}\" /F");
            
            client.Disconnect();

            return (true, new CommandExecutionResult
            {
                Success = result.ExitStatus == 0,
                Output = result.Result,
                Error = result.Error,
                ExitStatus = result.ExitStatus
            });
        }
        catch (Exception)
        {
            return (false, null);
        }
    }

    private async Task<CommandExecutionResult> ExecuteWindowsViaReverse(Connection connection, Command command)
    {
        // Implementation for reverse connection to Windows
        var reverseService = new WindowsReverseService();
        
        try
        {
            // The target Windows machine connects to our relay server
            var session = await reverseService.WaitForConnection(connection);
            
            // Execute command through the reverse connection
            var result = await session.ExecuteCommand(ConvertToWindowsCommand(command));
            
            return new CommandExecutionResult
            {
                Success = result.Success,
                Output = result.Output,
                Error = result.Error,
                ExitStatus = result.ExitCode
            };
        }
        catch (Exception ex)
        {
            return new CommandExecutionResult
            {
                Success = false,
                Error = $"Reverse connection failed: {ex.Message}",
                ExitStatus = -1
            };
        }
    }

    private string ConvertToWindowsCommand(Command command)
    {
        // Convert Linux-style commands to Windows equivalents if needed
        var cmd = command.TerminalCommand;
        
        // Common conversions
        var conversions = new Dictionary<string, string>
        {
            { "ls", "dir" },
            { "cp", "copy" },
            { "mv", "move" },
            { "rm", "del" },
            { "mkdir", "md" },
            { "pwd", "cd" },
            { "cat", "type" },
            { "grep", "findstr" },
            { "ps", "tasklist" },
            { "kill", "taskkill /F /PID" }
        };

        foreach (var conversion in conversions)
        {
            if (cmd.StartsWith(conversion.Key + " ") || cmd == conversion.Key)
            {
                cmd = cmd.Replace(conversion.Key, conversion.Value);
                break;
            }
        }

        return cmd;
    }
}

// Supporting classes for reverse connections
public class ReverseTunnelService
{
    private readonly string _relayServerHost = "relay.yourservice.com";
    private readonly int _relayServerPort = 8443;

    public async Task<ITunnel> EstablishTunnel(Connection connection)
    {
        // Implementation would establish a reverse SSH tunnel
        // The target machine connects out to your relay server
        // No inbound ports need to be opened on the target
        
        var tunnel = new SshReverseTunnel(_relayServerHost, _relayServerPort);
        await tunnel.ConnectAsync(connection);
        return tunnel;
    }
}

public class WindowsReverseService
{
    private readonly string _relayServerHost = "relay.yourservice.com";
    private readonly int _relayServerPort = 8444;

    public async Task<IWindowsSession> WaitForConnection(Connection connection)
    {
        // Implementation would wait for the Windows machine to connect
        // This could be done via a lightweight agent or PowerShell script
        // that the user runs once on their Windows machine
        
        var session = new WindowsReverseSession(_relayServerHost, _relayServerPort);
        await session.WaitForClientAsync(connection.Id);
        return session;
    }
}

// Interfaces for the tunnel implementations
public interface ITunnel
{
    Task<TunnelExecutionResult> ExecuteCommand(string command);
}

public interface IWindowsSession
{
    Task<WindowsExecutionResult> ExecuteCommand(string command);
}

public class TunnelExecutionResult
{
    public bool Success { get; set; }
    public string Output { get; set; }
    public string Error { get; set; }
    public int ExitCode { get; set; }
}

public class WindowsExecutionResult
{
    public bool Success { get; set; }
    public string Output { get; set; }
    public string Error { get; set; }
    public int ExitCode { get; set; }
}

// Placeholder implementations
public class SshReverseTunnel : ITunnel
{
    private readonly string _host;
    private readonly int _port;

    public SshReverseTunnel(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public async Task ConnectAsync(Connection connection)
    {
        // Implement reverse SSH tunnel logic
        await Task.CompletedTask;
    }

    public async Task<TunnelExecutionResult> ExecuteCommand(string command)
    {
        // Execute command through tunnel
        await Task.CompletedTask;
        return new TunnelExecutionResult();
    }
}

public class WindowsReverseSession : IWindowsSession
{
    private readonly string _host;
    private readonly int _port;

    public WindowsReverseSession(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public async Task WaitForClientAsync(Guid connectionId)
    {
        // Wait for Windows client to connect
        await Task.CompletedTask;
    }

    public async Task<WindowsExecutionResult> ExecuteCommand(string command)
    {
        // Execute command on Windows
        await Task.CompletedTask;
        return new WindowsExecutionResult();
    }
}