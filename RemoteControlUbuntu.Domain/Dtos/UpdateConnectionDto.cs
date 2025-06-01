namespace RemoteControlUbuntu.Domain.DTOs;

public class UpdateConnectionDto
{
    public Guid ConnectionId { get; set; }

    public string Name { get; set; }

    public string Host { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}