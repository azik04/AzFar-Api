using Final.DAL.Repositories;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.OrderTimes;

public class OrderTimeService : IOrderTimeService
{
    private readonly IBaseRepository<OrderTime> _timeRepository;

    public OrderTimeService(IBaseRepository<OrderTime> timeRepository)
    {
        _timeRepository = timeRepository;
    }
    public IBaseResponse<List<OrderTime>> GetOrderTimes()
    {
        try
        {
            var order = _timeRepository.GetAll().ToList();
            return new BaseResponse<List<OrderTime>>()
            {
                Data = order,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<OrderTime>>()
            {
                Description = $"[GetOrders] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
