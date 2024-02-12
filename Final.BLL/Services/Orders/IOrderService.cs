using Final.Domain.Entity;
using Final.Domain.ViewModel.Orders;
using Final.Domain.ViewModel.Stadiums;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Orders;
public interface IOrderService
{
    Task<IBaseResponse<Order>> Create(Order model);
    IBaseResponse<List<OrderVM>> GetOrders();
    Task<IBaseResponse<List<GetOrderVM>>> GetOrder(long id);
    Task<IBaseResponse<Order>> DelateOrder(long id);
}
