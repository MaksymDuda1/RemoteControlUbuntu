namespace RemoteControlUbuntu.Domain.Dtos;

public class BlackListCommandDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<string> Commands { get; set; }
}