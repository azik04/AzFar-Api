using Final.Domain.Enum;
using static Final.Domain.Response.IBaseResponse;

namespace Final.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }
    public T Data { get; set; }
    public StatusCode StatusCode { get; set; }
};
