using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure.Entities;

public class User : IdentityUser<Guid>
{
    [MaxLength(100)]
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiration { get; set; }
    
    public List<Connection> Connections { get; set; } = new List<Connection>();

    public List<Command> UsersCommands { get; set; } = new List<Command>();
}