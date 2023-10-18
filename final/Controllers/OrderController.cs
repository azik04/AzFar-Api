using Final.BLL.Services.Orders;
using Final.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpGet]
    public IActionResult GetOrders()
    {
        var response = _orderService.GetOrders();
        return Ok(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order model)
    {
            var response = await _orderService.Create(model);
            if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = response.Description });
            }
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
