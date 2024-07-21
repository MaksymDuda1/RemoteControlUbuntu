namespace RemoteControlUbuntu.Domain.Dtos;

public class UpdateCommandDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string TerminalCommand { get; set; } = null!;

    public string? IconPath { get; set; }
}