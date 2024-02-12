using Final.BLL.Services.Orders;
using Final.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
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
        if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
        {
            return Ok(response.Data);
        }
        return BadRequest($"{response.Description}");
    }
    [HttpGet]
    [Route("GetOrder")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var response = await _orderService.GetOrder(id);
        if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
        {
            return Ok(response.Data);
        }
        else
        {
            return NotFound(response.Description);
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DelateOrder(long id)
    {
        var response = await _orderService.DelateOrder(id);
        if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
        {
            return Ok(response.Description);
        }
        else
        {
            return NotFound(response.Description);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order model)
    {
        var response = await _orderService.Create(model);
        if (response.StatusCode == Final.Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest($"{response.Description}");
    }
}
