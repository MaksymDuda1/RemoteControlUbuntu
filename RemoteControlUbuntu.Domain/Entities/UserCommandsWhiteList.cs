namespace RemoteControlUbuntu.Domain.Entities;

public class UserCommandsWhiteList
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Command { get; set; }
}