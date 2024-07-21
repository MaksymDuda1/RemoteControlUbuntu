using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface IConnectionService
{
    Task<List<ConnectionDto>> GetAllUserConnections(Guid userId);
    Task<ConnectionDto> GetConnectionById(Guid connectionId);
    Task AddConnection(AddConnectionDto connectionDto);
    Task UpdateConnection(UpdateConnectionDto updateConnectionDto);
    Task DeleteConnection(Guid connectionId);
}