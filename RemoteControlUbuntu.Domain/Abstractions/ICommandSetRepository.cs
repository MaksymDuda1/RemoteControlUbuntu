using System.Linq.Expressions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Abstractions;

public interface ICommandSetRepository
{
    Task<List<CommandSet>> GetAllAsync(
        params Expression<Func<CommandSet, object>>[] includes);

    Task<List<CommandSet>> GetByConditionsAsync(
        Expression<Func<CommandSet, bool>> expression,
        params Expression<Func<CommandSet, object>>[] includes);

    Task<CommandSet?> GetSingleByConditionAsync(
        Expression<Func<CommandSet, bool>> expression,
        params Expression<Func<CommandSet, object>>[] includes);

    Task InsertAsync(CommandSet entity);
    Task InsertRangeAsync(List<CommandSet> entities);
    void Update(CommandSet entity);
    Task Delete(Guid id);
    void Delete(CommandSet entity);
}