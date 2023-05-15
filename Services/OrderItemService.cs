using MarketplaceBackend.Data;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly UserDbContext _userDbContext;

        public OrderItemService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public OrderItem CreateOrderItem(OrderItemDTO orderItem)
        {
            var newOrderItem = new OrderItem
            {
                OrderID = orderItem.OrderID,
                ProductID = orderItem.ProductID,
                Count = orderItem.Count
            };
            _userDbContext.OrderItems.Add(newOrderItem);
            _userDbContext.SaveChanges();

            return _userDbContext.OrderItems.FirstOrDefault(c => c.ID == newOrderItem.ID);
        }

        public void Delete(int id)
        {
            var orderItem = _userDbContext.OrderItems.FirstOrDefault(p => p.ID == id);
            _userDbContext.OrderItems.Remove(orderItem);
            _userDbContext.SaveChanges();
        }

        public List<OrderItem> GetList()
        {
            return _userDbContext.OrderItems.ToList();
        }

        public OrderItem UpdateOrderItem(OrderItemDTO orderItem)
        {
            var newOrderItem = new OrderItem
            {
                OrderID = orderItem.OrderID,
                ProductID = orderItem.ProductID,
                Count = orderItem.Count
            };
            _userDbContext.OrderItems.Update(newOrderItem);
            _userDbContext.SaveChanges();

            return _userDbContext.OrderItems.FirstOrDefault(c => c.ID == newOrderItem.ID);
        }
    }
}
