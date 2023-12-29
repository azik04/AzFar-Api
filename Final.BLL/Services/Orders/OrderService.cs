using Final.BLL.Services.OrderTimes;
using Final.BLL.Services.Stadiums;
using Final.DAL.Repositories;
using Final.DAL.Repositories.Stadiums;
using Final.DAL.Repositories.Users;
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
    private readonly IUserRepository _userRepository;
    public OrderService(IBaseRepository<Order> orderRepository, IStadiumService stadiumService, IOrderTimeService orderTimeService, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _stadiumService = stadiumService;
        _orderTimeService = orderTimeService;
        _userRepository = userRepository;
    }

    public async Task<IBaseResponse<Order>> Create(Order model)
    {
        try
        {
            var order = new Order()
            {
                Id = model.Id,
                FullName = model.FullName,
                StadiumId = model.StadiumId,
                OrderTimeId = model.OrderTimeId,
                DateCreated = DateTime.Now
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
                var userName= _userRepository.GetById(orders[i].FullName).Result.Name;
                var orderTime = _orderTimeService.GetTime(orders[i].OrderTimeId).Result.Data.OrderTimes;
                var newOrd = new OrderVM
                {
                    StadiumId = stadiumName,
                    DateCreated = DateTime.Now,
                    OrderTimeId= orderTime,
                    FullName = userName,
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
