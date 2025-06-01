namespace RemoteControlUbuntu.Domain.Dtos;

public class AddCommandDto
{
    public string Name { get; set; } = null!;
    
    public string TerminalCommand { get; set; } = null!;

    public string Type { get; set; }

    public string? IconPath { get; set; }
    public Guid? UserId { get; set; }
    
}