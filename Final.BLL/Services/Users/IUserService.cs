using Final.Domain.Entity;
using Final.Domain.Response;
using Final.Domain.ViewModel.Users;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Users;

public interface IUserService
{
    Task<IBaseResponse<User>> Create(UserViewModel model);

    BaseResponse<Dictionary<int, string>> GetRoles();

    Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
}
