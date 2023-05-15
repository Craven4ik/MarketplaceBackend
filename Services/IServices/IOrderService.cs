using MarketPlace.Models;
using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services.IServices
{
    public interface IOrderService
    {
        List<Order> GetList();
        void Delete(int id);
        Order CreateOrder(OrderDTO order);
        Order UpdateOrder(OrderDTO order);
        Order FindOrderByEmail(Guid UserID);
    }
}
