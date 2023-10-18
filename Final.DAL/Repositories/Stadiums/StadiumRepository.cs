using Final.Domain.Entity;

namespace Final.DAL.Repositories.Stadiums;

public class StadiumRepository : IBaseRepository<Stadium>
{
    private readonly ApplicationDbContext _db;

    public StadiumRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task Create(Stadium entity)
    {
        await _db.Stadiums.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public IQueryable<Stadium> GetAll()
    {
        return _db.Stadiums;
    }

    public async Task Delete(Stadium entity)
    {
        _db.Stadiums.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Stadium> Update(Stadium entity)
    {
        _db.Stadiums.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
