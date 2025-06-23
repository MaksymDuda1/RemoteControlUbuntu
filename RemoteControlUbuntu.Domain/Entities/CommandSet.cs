namespace RemoteControlUbuntu.Domain.Entities;

public class CommandSet
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
    
    public List<Command> Commands { get; set; }
}