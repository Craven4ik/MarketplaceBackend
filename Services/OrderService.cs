using MarketPlace.Models;
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
    private readonly IItemService _itemService;
    private readonly IOrderItemService _orderItemService;

    public OrderService(UserDbContext userDbContext,
        IUserService userService,
        IOrderItemService orderItemService,
        IItemService itemService)
    {
        _userDbContext = userDbContext;
        _userService = userService;
        _orderItemService = orderItemService;
        _itemService = itemService;
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
        //var query = _userDbContext.Orders.AsNoTracking();

        if (filter.UserID != null)
            query = query.Where(c=> c.UserId.Equals(filter.UserID));

        if (filter.State.HasValue)
            query = query.Where(c => c.State == filter.State);

        return query.ToList();
    }

    public bool IsExistsByUserIDInProgress(string userID)
    {
        return _userDbContext.Orders.Any(c=> c.User.Id == userID && c.State == StateOrder.InProgress);
    }

    public Order UpdateOrder(OrderDTO order)
    {
        var newOrder = Map(order);
        _userDbContext.Orders.Update(newOrder);
        _userDbContext.SaveChanges();

        return _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.ID == newOrder.ID);
    }

    public List<Order> GetListByUserID(string userID)
    {
        return _userDbContext.Orders.Where(p => p.User.Id == userID).ToList();
    }

    public Order GetByUserID(string userID)
    {
        return _userDbContext.Orders.Include(c => c.OrderItems).FirstOrDefault(c => c.UserId.Equals(userID));
    }

    private Order Map(OrderDTO dto) =>
        new Order
        {
            User = _userService.FindUserById(dto.UserID),
            State = dto.State,
            OrderItems = dto.OrderItems.Select(x => new OrderItem
            {
                OrderID = x.OrderID,
                ProductID = x.ProductID,
                Count = x.Count
            })
            .ToList() ?? new List<OrderItem>()
        };

    public void AddToCart(string UserId, int ItemId)
    {
        _userDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        var order = GetByUserID(UserId);

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
