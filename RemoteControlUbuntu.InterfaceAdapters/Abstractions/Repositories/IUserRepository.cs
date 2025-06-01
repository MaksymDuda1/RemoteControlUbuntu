using System.Linq.Expressions;
using RemoteControlUbuntu.Infrastructure.Entities;

namespace RemoteControlUbuntu.Infrastructure.Abstractions.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(
        params Expression<Func<User, object>>[] includes);

    Task<List<User>> GetByConditionsAsync(
        Expression<Func<User, bool>> expression,
        params Expression<Func<User, object>>[] includes);

    Task<User?> GetSingleByConditionAsync(
        Expression<Func<User, bool>> expression,
        params Expression<Func<User, object>>[] includes);

    Task InsertAsync(User entity);
    Task InsertRangeAsync(List<User> entities);
    void Update(User entity);
    Task Delete(Guid id);
    void Delete(User entity);
}