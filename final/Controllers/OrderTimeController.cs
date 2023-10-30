using Final.BLL.Services.Orders;
using Final.BLL.Services.OrderTimes;
using Final.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTimeController : ControllerBase
    {
        private readonly IOrderTimeService _timeService;

        public OrderTimeController(IOrderTimeService timeService)
        {
            _timeService = timeService;
        }
        [HttpGet]
        public IActionResult GetOrderTimes()
        {
            var response = _timeService.GetOrderTimes();
            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok(response.Data);
            }

            return BadRequest($"{response.Description}");
        }
    }
}
