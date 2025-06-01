namespace RemoteControlUbuntu.Domain.DTOs;

public class AddCommandDto
{
    public string Name { get; set; } = null!;
    
    public string TerminalCommand { get; set; } = null!;

    public string? IconPath { get; set; }
    public Guid? UserId { get; set; }
    
}