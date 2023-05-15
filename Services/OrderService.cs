using MarketplaceBackend.Data;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services;

public class OrderService : IOrderService
{
    private readonly UserDbContext _userDbContext;

    public OrderService(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public Order CreateOrder(OrderDTO order)
    {
        var newOrder = Map(order);
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
        var newOrder = Map(order);
        _userDbContext.Orders.Update(newOrder);
        _userDbContext.SaveChanges();

        return _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.ID == newOrder.ID);
    }

    public List<Order> FindOrderByUserID(Guid userID)
    {
        return _userDbContext.Orders.Where(p => p.UserID == userID).ToList();
    }

    private Order Map(OrderDTO dto) =>
        new Order
        {
            UserID = dto.UserID,
            State = dto.State,
            OrderItems = dto.OrderItems.Select(x => new OrderItem
            {
                OrderID = x.OrderID,
                ProductID = x.ProductID,
                Count = x.Count
            })
            .ToList() ?? new List<OrderItem>()
        };
}
