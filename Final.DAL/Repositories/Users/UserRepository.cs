using Final.Domain.Entity;

namespace Final.DAL.Repositories.Users;

public class UserRepository : IBaseRepository<User>
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<bool> Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(User entity)
    {
        _db.Users.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<User> GetAll()
    {
        return _db.Users;
    }

    public async Task<User> Update(User entity)
    {
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
