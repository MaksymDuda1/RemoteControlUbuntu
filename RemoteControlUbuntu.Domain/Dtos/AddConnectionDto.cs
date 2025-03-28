using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Dtos;

public class AddConnectionDto
{
    public string Name { get; set; }
    
    public string Host { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public List<Command> StartUpCommands { get; set; } = new List<Command>();

    public Guid UserId { get; set; }

}