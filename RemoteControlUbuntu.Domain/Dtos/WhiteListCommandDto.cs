using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RemoteControlUbuntu.Domain.Dtos;

public class WhiteListCommandDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<string> Commands { get; set; }
}