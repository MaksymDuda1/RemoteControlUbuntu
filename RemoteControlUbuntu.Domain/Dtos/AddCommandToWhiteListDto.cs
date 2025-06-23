namespace RemoteControlUbuntu.Domain.Dtos;

public class AddCommandToWhiteListDto
{
    public string Name { get; set; }

    public List<string> Commands { get; set; }
}