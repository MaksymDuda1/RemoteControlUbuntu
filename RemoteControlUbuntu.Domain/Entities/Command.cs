namespace RemoteControlUbuntu.Domain.Entities;

public partial class Command
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string TerminalCommand { get; set; } = null!;

    public string? IconPath { get; set; }

    public Guid? UserId { get; set; }
}