using Final.Domain.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using Final.DAL.Repositories.Users;
using Final.Domain.Entity;
using Final.Domain.Helpers;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AccountController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel vm)
            
        {
            var user = new User
            {
                Name = vm.Name,
                Phone = vm.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password)
            };
            await _repository.Create(user);
            return Ok(vm);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel vm)
        {
            var user = _repository.GetByPhone(vm.Phone);

            if (user == null) return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(vm.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _repository.GetById(userId).Result;

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
