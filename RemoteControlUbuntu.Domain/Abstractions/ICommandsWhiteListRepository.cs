using System.Linq.Expressions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Abstractions;

public interface ICommandsWhiteListRepository
{
    Task<List<CommandsWhiteList>> GetAllAsync(
        params Expression<Func<CommandsWhiteList, object>>[] includes);

    Task<List<CommandsWhiteList>> GetByConditionsAsync(
        Expression<Func<CommandsWhiteList, bool>> expression,
        params Expression<Func<CommandsWhiteList, object>>[] includes);

    Task<CommandsWhiteList?> GetSingleByConditionAsync(
        Expression<Func<CommandsWhiteList, bool>> expression,
        params Expression<Func<CommandsWhiteList, object>>[] includes);

    Task InsertAsync(CommandsWhiteList entity);
    Task InsertRangeAsync(List<CommandsWhiteList> entities);
    void Update(CommandsWhiteList entity);
    Task Delete(Guid id);
    void Delete(CommandsWhiteList entity);
}