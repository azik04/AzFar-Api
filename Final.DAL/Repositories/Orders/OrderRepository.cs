using Final.Domain.Entity;

namespace Final.DAL.Repositories.Orders;

public class OrderRepository : IBaseRepository<Order>
{
    private readonly ApplicationDbContext _db;

    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<bool> Create(Order entity)
    {
        await _db.Orders.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public IQueryable<Order> GetAll()
    {
        return _db.Orders;
    }

    public async Task<bool> Delete(Order entity)
    {
        _db.Orders.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Order> Update(Order entity)
    {
        _db.Orders.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
