using Final.Domain.Entity;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Stadiums;

public interface IStadiumService
{
    IBaseResponse<List<Stadium>> GetStadiums();
    Task<IBaseResponse<Stadium>> GetStadium(long id);
    Task<IBaseResponse<Stadium>> CreateStadium(Stadium car, byte[] imageData);
    Task<IBaseResponse<bool>> DeleteStadium(long id);
}
