using Final.Domain.Entity;
using Final.Domain.ViewModel.Orders;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Orders;
public interface IOrderService
{
    Task<IBaseResponse<Order>> Create(Order model);
    IBaseResponse<List<OrderVM>> GetOrders();
}
