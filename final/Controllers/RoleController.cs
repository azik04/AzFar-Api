using Final.BLL.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) 
        {
        _roleService = roleService;
        }
        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetOrderTimes()
        {
            var response = _roleService.GetRoles();
            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }

            return BadRequest($"{response.Description}");
        }
    }
}
