using Final.BLL.Services.Stadiums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumPhotoController : ControllerBase
    {
        private readonly IStadiumService _stadiumService;

        public StadiumPhotoController(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }
        [HttpGet]
        [Route("GetStadiums")]
        public IActionResult GetStadiums()
        {
            var response = _stadiumService.GetStadiums();
            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }

            return BadRequest($"{response.Description}");
        }
    }
}
