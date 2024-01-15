using Final.BLL.Services.OrderTimes;
using Final.BLL.Services.Stadiums;
using Final.DAL.Repositories;
using Final.DAL.Repositories.StadiumPhoto;
using Final.DAL.Repositories.Stadiums;
using Final.DAL.Repositories.Users;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using Final.Domain.ViewModel.Orders;
using Final.Domain.ViewModel.Stadiums;
using static Final.Domain.Response.IBaseResponse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Final.BLL.Services.Orders;
public class OrderService : IOrderService
{
    private readonly IBaseRepository<Order> _orderRepository;
    private readonly IStadiumService _stadiumService;
    private readonly IOrderTimeService _orderTimeService;
    private readonly IUserRepository _userRepository;
    private readonly IBaseRepository<Stadium> _stadiumRepository;
    private readonly IBaseRepository<StadiumPhotos> _stadiumPhotosRepository;
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

    public async Task<IBaseResponse<List<GetOrderVM>>> GetOrder(long id)
    {
        try
        {
            var orders = _orderRepository.GetAll().Where(o => o.FullName == id).ToList();
            if (orders.Count == 0)
            {
                return new BaseResponse<List<GetOrderVM>>()
                {
                    StatusCode = StatusCode.OrderNotFound,
                    Description = $"No orders found for id: {id}"
                };
            }

            var orderVMs = new List<GetOrderVM>();
            foreach (var order in orders)
            {
                var stadiumName = _stadiumService.GetStadium(order.StadiumId).Result.Data.Name;
                var stadiumAdress = _stadiumService.GetStadium(order.StadiumId).Result.Data.Adress;
                var stadiumPhoto = _stadiumService.GetStadium(order.StadiumId).Result.Data.StadiumPhoto;
                var userName = _userRepository.GetById(order.FullName).Result.Name;
                var orderTime = _orderTimeService.GetTime(order.OrderTimeId).Result.Data.OrderTimes;

                var newOrd = new GetOrderVM
                {
                    StadiumId = stadiumName,
                    DateCreated = order.DateCreated,
                    OrderTimeId = orderTime,
                    FullName = userName,
                    StadiumAdress = stadiumAdress,
                    StadiumPhoto = stadiumPhoto
                };

                orderVMs.Add(newOrd);
            }

            return new BaseResponse<List<GetOrderVM>>()
            {
                StatusCode = StatusCode.OK,
                Data = orderVMs
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<GetOrderVM>>()
            {
                Description = $"[GetOrder] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

}