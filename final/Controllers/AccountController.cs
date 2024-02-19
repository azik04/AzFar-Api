using Final.Domain.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using Final.DAL.Repositories.Users;
using Final.Domain.Entity;
using Final.Domain.Helpers;
using Final.DAL.Repositories;
using System.Security.Claims;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        private readonly IBaseRepository<Role> _role;

        public AccountController(IUserRepository repository, JwtService jwtService, IBaseRepository<Role> role)
        {
            _repository = repository;
            _jwtService = jwtService;
            _role = role;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "TEST");
            }

            var user = new User
            {
                Name = vm.Name,
                Phone = vm.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                RoleId = 1
            };
            if (_repository.GetAll().FirstOrDefault(x => x.Phone == x.Phone ) != null)
            {
                return BadRequest(new { message = "User with Same number exists" });
            }
            await _repository.Create(user);
            return Ok(vm);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Phone) || string.IsNullOrEmpty(vm.Password))
            {
                ModelState.AddModelError("", "Номер телефона и пароль обязательны для заполнения");
            }

            var user = _repository.GetByPhone(Convert.ToInt32(vm.Phone));

            if (user == null) return BadRequest(new { message = "Phone is wrong" });

            if (!BCrypt.Net.BCrypt.Verify(vm.Password, user.Password))
            {
                return BadRequest(new { message = "Phone or password are wrong" });
            }

            var jwt = _jwtService.Generate(user.Id, user.RoleId, user.Name);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success",
                token = jwt
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