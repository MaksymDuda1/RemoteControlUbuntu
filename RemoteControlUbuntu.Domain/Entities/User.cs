using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RemoteControlUbuntu.Domain.Entities;

public class User : IdentityUser<Guid>
{
    [MaxLength(100)]
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiration { get; set; }
    
    public List<Connection> Connections { get; set; } = [];

    public List<UserCommandsWhiteList> UserWhiteList { get; set; }

    public List<Command> UsersCommands { get; set; } = [];
    
    
}