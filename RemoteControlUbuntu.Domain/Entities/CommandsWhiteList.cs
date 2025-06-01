namespace RemoteControlUbuntu.Domain.Entities;

public class CommandsWhiteList
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<string> Commands { get; set; }
}