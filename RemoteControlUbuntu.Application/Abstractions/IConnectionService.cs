using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Models;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface IConnectionService
{
    Task<PaginatedList<ConnectionDto>> GetAllUserConnections(ConnectionFilterModel connectionFilterModel);
    Task<ConnectionDto> GetConnectionById(Guid connectionId);
    Task<Guid> AddConnection(AddConnectionDto connectionDto);
    Task UpdateConnection(UpdateConnectionDto updateConnectionDto);
    Task DeleteConnection(Guid connectionId);
}