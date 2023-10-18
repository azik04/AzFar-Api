using Final.Domain.Enum;
namespace Final.Domain.Response;

public interface IBaseResponse
{
    public interface IBaseResponse<T>
    {
        string Description { get; set; }
        T Data { get; set; }
        StatusCode StatusCode { get; }
    }
}
