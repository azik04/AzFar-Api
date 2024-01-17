using Final.Domain.Entity;
using Final.Domain.ViewModel.Stadiums;
using Microsoft.AspNetCore.Http;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Stadiums;

public interface IStadiumService
{
    IBaseResponse<List<Stadium>> GetStadiums();
    Task<IBaseResponse<GetStadiumViewModel>> GetStadium(long id);
    Task<IBaseResponse<Stadium>> CreateStadium(StadiumViewModel model);
    Task<IBaseResponse<bool>> DeleteStadium(long id);
}
