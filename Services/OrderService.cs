using MarketplaceBackend.Data;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services;

public class OrderService : IOrderService
{
    private readonly UserDbContext _userDbContext;
    private readonly IUserService _userService;

    public OrderService(UserDbContext userDbContext, IUserService userService)
    {
        _userDbContext = userDbContext;
        _userService = userService;
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
    public Order GetCurrentByUserID(string userID)
        => _userDbContext.Orders.FirstOrDefault(c => c.User.Id == userID && c.State == StateOrder.InProgress);

    public List<Order> GetList(OrderFilter filter)
    {
        var query = _userDbContext.Orders.Include(c => c.OrderItems).AsNoTracking();

        if (filter.UserID != null)
            query = query.Where(c=> c.UserId.Equals(filter.UserID));

        if (filter.State.HasValue)
            query = query.Where(c => c.State == filter.State);

        return query.ToList();
    }

    public bool IsExistsByUserIDInProgress(string userID)
        => _userDbContext.Orders.Any(c => c.User.Id == userID && c.State == StateOrder.InProgress);

    public Order UpdateOrder(OrderDTO order)
    {
        _userDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        var newOrder = Map(order);
        _userDbContext.Attach(newOrder);
        _userDbContext.Orders.Update(newOrder);
        _userDbContext.SaveChanges();

        return _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.ID == newOrder.ID);
    }

    public List<Order> GetListByUserID(string userID)
        => _userDbContext.Orders.Where(p => p.User.Id == userID).ToList();

    public Order GetByUserID(string userID)
        => _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.UserId.Equals(userID));

    private Order Map(OrderDTO dto) =>
        new Order
        {
            ID = dto.ID,
            User = _userService.FindUserById(dto.UserID),
            State = dto.State,
            OrderItems = _userDbContext.OrderItems.Where(c => c.OrderID == dto.ID).ToList()
        };

    public void AddToCart(string UserId, int ItemId)
    {
        _userDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        var order = GetList(new OrderFilter
        {
            UserID = UserId,
            State = StateOrder.InProgress
        }).FirstOrDefault();

        if (order != null)
        {
            var orderItem = _userDbContext.OrderItems
                .FirstOrDefault(c => c.OrderID == order.ID && c.ProductID == ItemId);
            if (orderItem != null)
                _userDbContext.OrderItems.Update(new OrderItem
                {
                    ID = orderItem.ID,
                    OrderID = orderItem.OrderID,
                    ProductID = orderItem.ProductID,
                    Count = orderItem.Count + 1
                });
            else
            {
                _userDbContext.OrderItems.Add(new OrderItem
                {
                    OrderID = order.ID,
                    ProductID = ItemId,
                    Count = 1
                });
            };
        }
        else
        {
            var newOrderItem = new OrderItem
            {
                ProductID = ItemId,
                Count = 1,
            };

            var newOrder = new Order
            {
                State = StateOrder.InProgress,
                User = _userService.FindUserById(UserId),
                OrderItems = new List<OrderItem>(),
            };
            newOrderItem.Order = newOrder;
            _userDbContext.Entry(newOrder.User).State = EntityState.Unchanged;
            newOrder.OrderItems.Add(newOrderItem);
            _userDbContext.Orders.Add(newOrder);

        }
        _userDbContext.SaveChanges();
    }
}
