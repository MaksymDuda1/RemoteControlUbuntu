using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.DTOs;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Infrastructure.Model;
using System.Linq.Expressions;

namespace RemoteControlUbuntu.Infrastructure.Abstractions.Repositories;

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