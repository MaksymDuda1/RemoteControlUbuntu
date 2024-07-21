using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Dtos;

public class UserConnectionCommandDto
{
    public Guid ConnectionId { get; set; }

    public Guid CommandId { get; set; }
}