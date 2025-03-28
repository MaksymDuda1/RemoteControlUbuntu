using System.Linq.Expressions;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Domain.Models;

namespace RemoteControlUbuntu.Domain.Abstractions;

public interface IConnectionRepository
{
    Task<PaginatedList<ConnectionDto>> GetUserConnections(ConnectionFilterModel connectionFilterModel);
    
    Task<List<Connection>> GetAllAsync(
        params Expression<Func<Connection, object>>[] includes);

    Task<List<Connection>> GetByConditionsAsync(
        Expression<Func<Connection, bool>> expression,
        params Expression<Func<Connection, object>>[] includes);

    Task<Connection?> GetSingleByConditionAsync(
        Expression<Func<Connection, bool>> expression,
        params Expression<Func<Connection, object>>[] includes);

    Task InsertAsync(Connection entity);
    Task InsertRangeAsync(List<Connection> entities);
    void Update(Connection entity);
    Task Delete(Guid id);
    void Delete(Connection entity);
}