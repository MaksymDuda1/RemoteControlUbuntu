using System.Text.Json.Serialization;

namespace RemoteControlUbuntu.Domain.Entities;

public class Command
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; }
    
    public string TerminalCommand { get; set; } = null!;

    public string? IconPath { get; set; }

    public Guid? UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
}