using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("api/orders")]
    public IActionResult GetAllOrders()
    {
        List<Order> orders = _orderService.GetAllOrders();
        return Ok(orders);
    }

    [Authorize(Roles = "Admin,Cheff")]
    [HttpPost("api/orders")]
    public IActionResult AddOrder([FromForm] Order order)
    {
        ModelState.Remove("Dish");
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError()));
        Order dbOrder = _orderService.AddOrder(order!);
        return Created("api/orders/" + dbOrder.OrderId, dbOrder);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("api/orders/{id}")]
    public IActionResult DeleteDish([FromRoute] int id)
    {
        bool success = _orderService.DeleteOrder(id);
        if (!success) return NotFound(new ResourceNotFound(id));
        return NoContent();
    }

}
