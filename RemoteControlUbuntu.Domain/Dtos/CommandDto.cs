using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Dtos;

public class CommandDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string TerminalCommand { get; set; } = null!;

    public Guid? UserId { get; set; }

    public User? User { get; set; }
}