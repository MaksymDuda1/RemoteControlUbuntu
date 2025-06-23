namespace RemoteControlUbuntu.Domain.Dtos;

public class AddCommandToBlackListDto
{
    public string Name { get; set; }

    public List<string> Commands { get; set; }
}

