using System.Text.Json.Serialization;

namespace RemoteControlUbuntu.Domain.Models;

public class CommandModel 
{ 
    [JsonPropertyName("general_command_name")]
    public string GeneralCommandName { get; set; } 
    
    [JsonPropertyName("terminal_command")]
    public string TerminalCommand { get; set; }
}