using Final.Domain.Entity;
using Final.Domain.ViewModel.Stadiums;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.OrderTimes;

public interface IOrderTimeService
{
    IBaseResponse<List<OrderTime>> GetOrderTimes();
    Task<IBaseResponse<OrderTime>> GetTime(long id);
}
