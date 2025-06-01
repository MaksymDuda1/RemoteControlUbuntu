namespace RemoteControlUbuntu.Application.Models;

public class CommandExecutionResult
{
    public bool Success { get; set; }
    
    public string Output { get; set; }
    
    public string Error { get; set; }
    
    public int? ExitStatus { get; set; }
}