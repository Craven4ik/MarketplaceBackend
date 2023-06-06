using MarketplaceBackend.Data;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Services;

public class OrderItemService : IOrderItemService
{
    private readonly UserDbContext _userDbContext;

    public OrderItemService(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }
    public OrderItem CreateOrderItem(OrderItemDTO orderItem)
    {
        var newOrderItem = Map(orderItem);
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
        => _userDbContext.OrderItems.ToList();
    

    public OrderItem UpdateOrderItem(OrderItemDTO orderItem)
    {
        var newOrderItem = Map(orderItem);

        if (newOrderItem.Count == 0)
        {
            Delete(newOrderItem.ID);
            return null;
        }
            
        _userDbContext.OrderItems.Update(newOrderItem);
        _userDbContext.SaveChanges();

        return _userDbContext.OrderItems.FirstOrDefault(c => c.ID == newOrderItem.ID);
    }

    private OrderItem Map(OrderItemDTO orderItem)
    => new OrderItem
    {
        ID = orderItem.ID,
        OrderID = orderItem.OrderID,
        ProductID = orderItem.ProductID,
        Count = orderItem.Count
    };

    public int OrderPrice(int id)
        => _userDbContext.OrderItems.Where(c => c.OrderID == id).Sum(c => c.Count * c.Product.Price);
}
