using Automarket.Domain.Enum;
using Final.DAL.Repositories;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Helpers;
using Final.Domain.Response;
using Final.Domain.ViewModel.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Final.BLL.Services.Accounts;

public class AccountServices : IAccounServices

{
    private readonly IBaseRepository<User> _userRepository;
    private readonly ILogger<AccountServices> _logger;

    public AccountServices(IBaseRepository<User> userRepository, ILogger<AccountServices> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == model.UserName);
            if (user == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.UserNotFound,
                    Description = "User is not found"
                };
            }

            user.PasswordHash = HashPasswordHelper.HashPassowrd(model.NewPassword);
            await _userRepository.Update(user);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = "Пароль обновлен"
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == model.Username);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Invalid password or login"
                };
            }

            if (user.PasswordHash != HashPasswordHelper.HashPassowrd(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Invalid password or login"
                };
            }
            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Username == model.Username);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь с таким логином уже есть",
                };
            }

            user = new User()
            {
                Username = model.Username,
                Role = Role.User,
                PasswordHash = HashPasswordHelper.HashPassowrd(model.Password),
            };

            await _userRepository.Create(user);
            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Объект добавился",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Register]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    private ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
            };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
