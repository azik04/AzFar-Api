using Final.DAL.Repositories;
using Final.DAL.Repositories.Stadiums;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Orders;
public class OrderService : IOrderService
{
    private readonly IBaseRepository<Order> _orderRepository;
    public OrderService(IBaseRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IBaseResponse<Order>> Create(Order model)
    {
        try
        {
            var order = new Order()
            {
                FullName = model.FullName,
                StadiumId = model.StadiumId,
                DateCreated = DateTime.Now,
                OrderTimeId = model.OrderTimeId,
            };

            await _orderRepository.Create(order);

            return new BaseResponse<Order>()
            {
                Description = "Заказ создан",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Order>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public IBaseResponse<List<Order>> GetOrders()
    {
        try
        {
            var order = _orderRepository.GetAll().ToList();
            return new BaseResponse<List<Order>>()
            {
                Data = order,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Order>>()
            {
                Description = $"[GetOrders] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

}
