using AutoMapper;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Exceptions;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Domain.Models;

namespace RemoteControlUbuntu.Application.Services;

public class ConnectionService(
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IConnectionService
{
    public async Task<PaginatedList<ConnectionDto>> GetAllUserConnections(ConnectionFilterModel connectionFilterModel)
    {
        var connections = await unitOfWork.Connections.GetUserConnections(connectionFilterModel);

        return connections;
    }

    public async Task<ConnectionDto> GetConnectionById(Guid connectionId)
    {
        var connection = await unitOfWork.Connections
            .GetSingleByConditionAsync(c => c.Id == connectionId);

        if (connection == null)
            throw new EntityNotFoundException("Connection Not Found");

        return mapper.Map<ConnectionDto>(connection);
    }

    public async Task AddConnection(AddConnectionDto connectionDto)
    {
        var user = await unitOfWork.Users
            .GetSingleByConditionAsync(u => u.Id == connectionDto.UserId);

        if (user == null)
            throw new EntityNotFoundException("User Not Found");

        var connection = mapper.Map<Connection>(connectionDto);
        
        await unitOfWork.Connections.InsertAsync(connection);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateConnection(UpdateConnectionDto updateConnectionDto)
    {
        var connection = await unitOfWork.Connections
            .GetSingleByConditionAsync(c => c.Id == updateConnectionDto.ConnectionId);

        if (connection == null)
            throw new EntityNotFoundException("Connection Not Found");

        connection.Name = updateConnectionDto.Name;
        connection.Host = updateConnectionDto.Host;
        connection.Username = updateConnectionDto.Username;
        connection.Password = connection.Password;

        unitOfWork.Connections.Update(connection);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteConnection(Guid connectionId)
    {
        await unitOfWork.Connections.Delete(connectionId);
        await unitOfWork.SaveAsync();
    }
}