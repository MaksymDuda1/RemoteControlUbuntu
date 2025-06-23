using System.Text.Json.Serialization;

namespace RemoteControlUbuntu.Domain.Entities;

public class Connection
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Host { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Os { get; set; } = "windows";
    
    public Guid UserId { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; } = null!;
}