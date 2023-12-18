using Final.BLL.Services.OrderTimes;
using Final.BLL.Services.Stadiums;
using Final.DAL.Repositories;
using Final.DAL.Repositories.Stadiums;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using Final.Domain.ViewModel.Orders;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Orders;
public class OrderService : IOrderService
{
    private readonly IBaseRepository<Order> _orderRepository;
    private readonly IStadiumService _stadiumService;
    private readonly IOrderTimeService _orderTimeService;
    public OrderService(IBaseRepository<Order> orderRepository, IStadiumService stadiumService, IOrderTimeService orderTimeService)
    {
        _orderRepository = orderRepository;
        _stadiumService = stadiumService;
        _orderTimeService = orderTimeService;
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

    public IBaseResponse<List<OrderVM>> GetOrders()
    {
        try
        {
            List<OrderVM> fOrders = new List<OrderVM>();
            var orders = _orderRepository.GetAll().ToList();

            for (int i = 0; i < orders.Count; i++)
            {
                var stadiumName = _stadiumService.GetStadium(orders[i].StadiumId).Result.Data.Name;
                //var orderTime = _orderTimeService.GetTime(orders[i].OrderTimeId).Result.Data.Name;
                var newOrd = new OrderVM
                {
                    Id = orders[i].Id,
                    StadiumId = stadiumName,
                    DateCreated = DateTime.Now,
                    //OrderTimeId= orderTime,
                    FullName = orders[i].FullName,
                };
                fOrders.Add(newOrd);
            }
            
            return new BaseResponse<List<OrderVM>>()
            {
                Data = fOrders,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<OrderVM>>()
            {
                Description = $"[GetOrders] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

}
