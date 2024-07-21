using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Dtos;

public class ConnectionCommandDto
{
    public Guid ConnectionId { get; set; }

    public Guid CommandId { get; set; }
}