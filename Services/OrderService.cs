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
    public Order GetCurrentByUserID(Guid userID)
        => _userDbContext.Orders.FirstOrDefault(c => c.UserID == userID && c.State == StateOrder.InProgress);
    public List<Order> GetList(OrderFilter filter)
    {
        var query = _userDbContext.Orders.Include(c => c.OrderItems).AsNoTracking();

        if(filter.UserID.HasValue)
            query = query.Where(c=> c.UserID == filter.UserID);

        if (filter.State.HasValue)
            query = query.Where(c => c.State == filter.State);

        return _userDbContext.Orders.ToList();
    }

    public bool IsExistsByUserIDInProgress(Guid userID)
    {
        return _userDbContext.Orders.Any(c=> c.UserID == userID && c.State == StateOrder.InProgress);
    }

    public Order UpdateOrder(OrderDTO order)
    {
        var newOrder = Map(order);
        _userDbContext.Orders.Update(newOrder);
        _userDbContext.SaveChanges();

        return _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.ID == newOrder.ID);
    }

    public List<Order> GetListByUserID(Guid userID)
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
