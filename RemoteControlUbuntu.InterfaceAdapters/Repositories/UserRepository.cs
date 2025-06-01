using RemoteControlUbuntu.Infrastructure.Abstractions.Repositories;
using RemoteControlUbuntu.Infrastructure.Entities;
using System.Linq.Expressions;

namespace RemoteControlUbuntu.Infrastructure.Repositories;

public class UserRepository(RemoteDbContext context)
    :BaseRepository<User>(context), IUserRepository
{
    public Task GetAllAsync(params Expression[] includes)
    {
        throw new NotImplementedException();
    }

    public Task GetByConditionsAsync(Expression expression, params Expression[] includes)
    {
        throw new NotImplementedException();
    }

    public Task GetSingleByConditionAsync(Expression expression, params Expression[] includes)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task InsertRangeAsync(List<User> entities)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(User entity)
    {
        throw new NotImplementedException();
    }
}