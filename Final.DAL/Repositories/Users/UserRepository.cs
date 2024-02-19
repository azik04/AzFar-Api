using Final.Domain.Entity;
using Microsoft.EntityFrameworkCore;

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
    public async Task<User> GetById(int id) =>
        await _db.Users.FirstOrDefaultAsync(u => u.Id == id);



    public async Task<bool> Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public IQueryable<User> GetAll()
    {
        return _db.Users;
    }

    public Task<bool> Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByPhoneandPasswod(int Phone, string Password)
    {
        return _db.Users.FirstOrDefaultAsync(u => u.Phone == Phone && u.Password == Password);
    }
}

