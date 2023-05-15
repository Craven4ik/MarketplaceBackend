using MarketPlace.Models;
using MarketplaceBackend.Data;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly UserDbContext _userDbContext;

        public OrderService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public Order CreateOrder(OrderDTO order)
        {
            var newOrder = new Order
            {
                UserID = order.UserID,
                State = order.State,
                OrderItems = order.OrderItems.Select(x => new OrderItem
                {
                    OrderID = x.OrderID,
                    ProductID = x.ProductID,
                    Count = x.Count
                })
                .ToList()
            };
            _userDbContext.Orders.Add(newOrder);
            _userDbContext.SaveChanges();

            return _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.ID == newOrder.ID);
        }

        public void Delete(int id)
        {
            var order = _userDbContext.Orders.FirstOrDefault(p => p.ID == id);
            _userDbContext.Orders.Remove(order);
            _userDbContext.SaveChanges();
        }

        public List<Order> GetList()
        {
            return _userDbContext.Orders.Include(c => c.OrderItems).ToList();
        }

        public Order UpdateOrder(OrderDTO order)
        {
            var newOrder = new Order
            {
                UserID = order.UserID,
                State = order.State,
                OrderItems = order.OrderItems.Select(x => new OrderItem
                {
                    OrderID = x.OrderID,
                    ProductID = x.ProductID,
                    Count = x.Count
                })
                .ToList()
            };
            _userDbContext.Orders.Update(newOrder);
            _userDbContext.SaveChanges();

            return _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.ID == newOrder.ID);
        }

        public Order FindOrderByEmail(Guid UserID)
        {
            var order = _userDbContext.Orders.FirstOrDefault(p => p.UserID == UserID);
            return new Order
            {
                UserID = order.UserID,
                State = order.State,
                User = order.User,
                OrderItems = order.OrderItems
            };
        }
    }
}
