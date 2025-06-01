using System.Linq.Expressions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Abstractions;

public interface ICommandsBlackListRepository
{
    Task<List<CommandsBlackList>> GetAllAsync(
        params Expression<Func<CommandsBlackList, object>>[] includes);

    Task<List<CommandsBlackList>> GetByConditionsAsync(
        Expression<Func<CommandsBlackList, bool>> expression,
        params Expression<Func<CommandsBlackList, object>>[] includes);

    Task<CommandsBlackList?> GetSingleByConditionAsync(
        Expression<Func<CommandsBlackList, bool>> expression,
        params Expression<Func<CommandsBlackList, object>>[] includes);

    Task InsertAsync(CommandsBlackList entity);
    Task InsertRangeAsync(List<CommandsBlackList> entities);
    void Update(CommandsBlackList entity);
    Task Delete(Guid id);
    void Delete(CommandsBlackList entity);
}