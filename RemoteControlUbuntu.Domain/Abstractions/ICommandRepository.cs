using System.Linq.Expressions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Abstractions;

public interface ICommandRepository
{
    Task<List<Command>> GetAllAsync(
        params Expression<Func<Command, object>>[] includes);

    Task<List<Command>> GetByConditionsAsync(
        Expression<Func<Command, bool>> expression,
        params Expression<Func<Command, object>>[] includes);

    Task<Command?> GetSingleByConditionAsync(
        Expression<Func<Command, bool>> expression,
        params Expression<Func<Command, object>>[] includes);

    Task InsertAsync(Command entity);
    Task InsertRangeAsync(List<Command> entities);
    void Update(Command entity);
    Task Delete(Guid id);
    void Delete(Command entity);
}