using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.DTOs;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Infrastructure.Model;
using IConnectionRepository = RemoteControlUbuntu.Infrastructure.Abstractions.Repositories.IConnectionRepository;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class ConnectionRepository(RemoteDbContext context) : BaseRepository<Connection>(context), IConnectionRepository
{
    public async Task<PaginatedList<ConnectionDto>> GetUserConnections(ConnectionFilterModel connectionFilterModel)
    {
        var connectionsQuery = GetFilteredConnections(connectionFilterModel);
        
        var dtos = connectionsQuery.Select(x => new ConnectionDto()
        {
            Id = x.Id,
            Name = x.Name,
            Username = x.Username,
            Host = x.Host,
            UserId = x.UserId
        });
        
        return await PaginatedList<ConnectionDto>.CreateAsync(dtos, connectionFilterModel.PageIndex, connectionFilterModel.PageSize);
    }

    private IQueryable<Connection> GetFilteredConnections(ConnectionFilterModel connectionFilterModel)
    {
        var connectionsQuery = context.Connections.Where(x => x.UserId == connectionFilterModel.UserId).AsQueryable();
        
        connectionsQuery = AddNameFiltering(connectionsQuery, connectionFilterModel.Name);
        
        connectionsQuery = AddHostFiltering(connectionsQuery, connectionFilterModel.Host);
        
        connectionsQuery = AddUsernameFiltering(connectionsQuery, connectionFilterModel.Username);
        
        return connectionsQuery;
    }

    private IQueryable<Connection> AddNameFiltering(IQueryable<Connection> connectionFilters, string? name)
    {
        return string.IsNullOrEmpty(name) ? connectionFilters : connectionFilters.Where(x => x.Name.Contains(name));
    }
    
    private IQueryable<Connection> AddHostFiltering(IQueryable<Connection> connectionFilters, string? host)
    {
        return string.IsNullOrEmpty(host) ? connectionFilters : connectionFilters.Where(x => x.Host.Contains(host));
    }
    
    private IQueryable<Connection> AddUsernameFiltering(IQueryable<Connection> connectionFilters, string? username)
    {
        return string.IsNullOrEmpty(username) ? connectionFilters : connectionFilters.Where(x => x.Username.Contains(username));
    }
    
    
}