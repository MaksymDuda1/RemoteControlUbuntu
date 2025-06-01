using System.Linq.Expressions;

namespace RemoteControlUbuntu.Application.Abstractions.Repositories;

public interface IBaseRepository<T>
{
    public Task<List<T>> GetAllAsync(
        params Expression<Func<T, object>>[] includes);

    public Task<List<T>> GetByConditionsAsync(
        Expression<Func<T, bool>> expression,
        params Expression<Func<T, object>>[] includes);

    public Task<T?> GetSingleByConditionAsync(
        Expression<Func<T, bool>> expression,
        params Expression<Func<T, object>>[] includes);

    public Task InsertAsync(T entity);
    public Task InsertRangeAsync(List<T> entities);
    public void Update(T entity);
    public Task Delete(Guid id);
    public void Delete(T entity);
}