using Final.Domain.Entity;
using Final.Domain.ViewModel.Stadiums;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Stadiums;

public interface IStadiumService
{
    IBaseResponse<List<Stadium>> GetStadiums();
    Task<IBaseResponse<Stadium>> GetStadium(long id);
    Task<IBaseResponse<Stadium>> CreateStadium(StadiumViewModel car, byte[] imageData);
    Task<IBaseResponse<bool>> DeleteStadium(long id);
}
