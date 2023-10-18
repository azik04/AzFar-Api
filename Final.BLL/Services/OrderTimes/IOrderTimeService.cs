using Final.Domain.Entity;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.OrderTimes;

public interface IOrderTimeService
{
    IBaseResponse<List<OrderTime>> GetOrderTimes();
}
