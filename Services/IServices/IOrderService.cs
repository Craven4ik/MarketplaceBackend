using MarketPlace.Models;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services.IServices;

public interface IOrderService
{
    List<Order> GetList(OrderFilter filter);
    Order GetCurrentByUserID(string userID);
    void Delete(int id);
    Order CreateOrder(OrderDTO order);
    Order UpdateOrder(OrderDTO order);
    void AddToCart(string UserId, int ItemId);
}