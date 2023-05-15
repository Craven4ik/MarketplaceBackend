using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public List<OrderItem> GetList()
        {
            return _orderItemService.GetList();
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            _orderItemService.Delete(id);
        }

        [HttpPost]
        public OrderItem Create(OrderItemDTO orderItem)
        {
            return _orderItemService.CreateOrderItem(orderItem);
        }

        [HttpPut("{id}")]
        public OrderItem Update(OrderItemDTO orderItem)
        {
            return _orderItemService.UpdateOrderItem(orderItem);
        }
    }
}
