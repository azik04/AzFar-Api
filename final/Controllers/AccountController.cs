using Final.BLL.Services.Accounts;
using Final.Domain.ViewModel.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccounServices _accountService;
        public AccountController(IAccounServices accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("api/register")] // Specify the route for the API endpoint
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var response = await _accountService.Register(model);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

            return Ok();
            
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
                var response = await _accountService.Login(model);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));
                return Ok();
        }
    }
}
