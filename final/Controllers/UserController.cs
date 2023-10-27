using Final.BLL.Services.Users;
using Final.Domain.Extention;
using Final.Domain.ViewModel.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }
            return BadRequest(new { errorMessage = response.Description });
        }
        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Create(model);
                if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
                {
                    return Ok(new { description = response.Description });
                }
                return BadRequest(new { errorMessage = response.Description });
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        //[HttpPost]
        //public JsonResult GetRoles()
        //{
        //    var types = _userService.GetRoles();
        //    return Ok(types.Data);
        //}
    }
}
