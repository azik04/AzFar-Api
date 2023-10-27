using Final.BLL.Services.Accounts;
using Final.Domain.ViewModel.Accounts;
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
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return Ok(model);
                }
                ModelState.AddModelError("", response.Description);
            }
            return BadRequest();
        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = await _accountService.Login(model);
//                if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
//                {
//                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
//                        new ClaimsPrincipal(response.Data));
//
//                    return Ok(model);
//                }
//                ModelState.AddModelError("", response.Description);
//            }
  //          return BadRequest();
//        }

//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Logout()
//        //{
//        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//        //    return Ok();
//        //}

//        [HttpPost]
//        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
//        {
//            //if (ModelState.IsValid)
//            //{
//                var response = await _accountService.ChangePassword(model);
//                //if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
//                //{
//                    return Ok(new { description = response.Description });
//                //}
//            //}
//            //var modelError = ModelState.Values.SelectMany(v => v.Errors);

//            //return BadRequest(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
//        }
    }
}