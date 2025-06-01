namespace RemoteControlUbuntu.Domain.Entities;

public class CommandsBlackList
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<string> Commands { get; set; }
}