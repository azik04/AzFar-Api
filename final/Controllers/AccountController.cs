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
        [HttpPost("api/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return Ok();
                }
                ModelState.AddModelError("", response.Description);
            }
            return BadRequest();
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
