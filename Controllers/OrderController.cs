using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{userID}")]
    public Order GetList([FromRoute] string userID)
        => _orderService.GetCurrentByUserID(userID);

    [HttpGet]
    public List<Order> GetList([FromQuery] OrderFilter filter)
        => _orderService.GetList(filter);

    [HttpDelete("{id}")]
    public void Delete([FromRoute] int id)
        => _orderService.Delete(id);

    [HttpPost]
    public Order Create(OrderDTO order)
        => _orderService.CreateOrder(order);

    [HttpPut("{id}")]
    public Order Update(OrderDTO order)
        => _orderService.UpdateOrder(order);

    [HttpPost("AddToCart")]
    public void AddToCart([FromBody] OrderInfo orderInfo)
    {
        _orderService.AddToCart(orderInfo.UserID, orderInfo.ItemID);
    }

    [HttpGet("GetOrdersWithFilter")]
    public List<Order> GetOrders([FromQuery]OrderFilter orderFilter)
    {
        return _orderService.GetList(orderFilter);
    }
}
