namespace RemoteControlUbuntu.Domain.Dtos;

public class ConnectionDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Host { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public Guid UserId { get; set; }
}