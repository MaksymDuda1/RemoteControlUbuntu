using System.Linq.Expressions;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Domain.Abstractions;

public interface IUserCommandsWhiteListRepository
{
    Task<List<UserCommandsWhiteList>> GetAllAsync(
        params Expression<Func<UserCommandsWhiteList, object>>[] includes);

    Task<List<UserCommandsWhiteList>> GetByConditionsAsync(
        Expression<Func<UserCommandsWhiteList, bool>> expression,
        params Expression<Func<UserCommandsWhiteList, object>>[] includes);

    Task<UserCommandsWhiteList?> GetSingleByConditionAsync(
        Expression<Func<UserCommandsWhiteList, bool>> expression,
        params Expression<Func<UserCommandsWhiteList, object>>[] includes);

    Task InsertAsync(UserCommandsWhiteList entity);
    Task InsertRangeAsync(List<UserCommandsWhiteList> entities);
    void Update(UserCommandsWhiteList entity);
    Task Delete(Guid id);
    void Delete(UserCommandsWhiteList entity);
}