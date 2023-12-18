using Final.DAL.Repositories;
using Final.DAL.Repositories.StadiumPhoto;
using Final.DAL.Repositories.Stadiums;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using Final.Domain.ViewModel.Stadiums;
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
    public async Task<IBaseResponse<OrderTime>> GetTime(long id)
    {
        try
        {
            var stadium = _timeRepository.GetAll().FirstOrDefault(x => x.Id == id);

            var data = new OrderTime()
            {
                OrderTimes = stadium.OrderTimes
            };

            return new BaseResponse<OrderTime>()
            {
                StatusCode = StatusCode.OK,
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<OrderTime>()
            {
                Description = $"[GetStadium] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
