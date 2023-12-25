using Final.Domain.Entity;

namespace Final.DAL.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public User GetByPhone(int phone)
    {
        return _db.Users.FirstOrDefault(u => u.Phone == phone);
    }

    public User GetById(int id)
    {
        return _db.Users.FirstOrDefault(u => u.Id == id);
    }

    async Task<bool> IBaseRepository<User>.Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public IQueryable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(User entity)
    {
        throw new NotImplementedException();
    }
}
