using Final.Domain.Response;
using Final.Domain.ViewModel.Accounts;
using System.Security.Claims;

namespace Final.BLL.Services.Accounts;

public interface IAccountService
{
    Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

    Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

    Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
}
