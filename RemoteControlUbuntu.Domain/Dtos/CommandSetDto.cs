using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Dtos;

public class CommandSetDto
{
      public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public List<Guid> CommandIds{ get; set; }
    
    public List<Command> Commands { get; set; }
}