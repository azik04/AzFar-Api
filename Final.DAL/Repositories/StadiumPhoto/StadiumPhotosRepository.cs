using Final.Domain.Entity;

namespace Final.DAL.Repositories.StadiumPhoto;

public class StadiumPhotosRepository : IBaseRepository<StadiumPhotos>
{
    private readonly ApplicationDbContext _db;

    public StadiumPhotosRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task Create(StadiumPhotos entity)
    {
        await _db.StadiumPhotos.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public IQueryable<StadiumPhotos> GetAll()
    {
        return _db.StadiumPhotos;
    }
    public async Task Delete(StadiumPhotos entity)
    {
        _db.StadiumPhotos.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<StadiumPhotos> Update(StadiumPhotos entity)
    {
        _db.StadiumPhotos.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
