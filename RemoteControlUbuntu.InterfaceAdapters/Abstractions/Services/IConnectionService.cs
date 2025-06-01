using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.DTOs;
using RemoteControlUbuntu.Infrastructure.Model;

namespace RemoteControlUbuntu.Infrastructure.Abstractions.Services;

public interface IConnectionService
{
    Task<PaginatedList<ConnectionDto>> GetAllUserConnections(ConnectionFilterModel connectionFilterModel);
    Task<ConnectionDto> GetConnectionById(Guid connectionId);
    Task<Guid> AddConnection(AddConnectionDto connectionDto);
    Task UpdateConnection(UpdateConnectionDto updateConnectionDto);
    Task DeleteConnection(Guid connectionId);
}