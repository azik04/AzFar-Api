using Final.Domain.Entity;

namespace Final.DAL.Repositories.Users;

public interface IUserRepository : IBaseRepository<User>
{
    User GetByPhone(int phone);
    Task<User> GetById(int id);
}
