using Automarket.Domain.Enum;
using Final.Domain.Entity;

namespace Final.DAL.Repositories.Roles;

public class RoleRepository : IBaseRepository<Role>
{
    private readonly ApplicationDbContext _db;
    public RoleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Role entity)
    {
        await _db.Roles.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(Role entity)
    {
        _db.Roles.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public IQueryable<Role> GetAll() => _db.Roles;

    public async Task<Role> Update(Role entity)
    {
        _db.Roles.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

}
