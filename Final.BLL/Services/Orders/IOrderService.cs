using Final.Domain.Entity;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Orders;
public interface IOrderService
{
    Task<IBaseResponse<Order>> Create(Order model);
    IBaseResponse<List<Order>> GetOrders();
}
