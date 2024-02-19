using Final.Domain.Entity;

namespace Final.DAL.Repositories.Users;

public interface IUserRepository : IBaseRepository<User>
{
    User GetByPhone(int phone);
    //Task<User> GetUserByPhoneAndPassword(string phone, string password);
    Task<User> GetById(int id);
    Task<User> GetByPhoneandPasswod(int Phone, string Password);
}
