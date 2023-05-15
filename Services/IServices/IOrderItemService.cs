using MarketplaceBackend.DTO;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services.IServices
{
    public interface IOrderItemService
    {
        List<OrderItem> GetList();
        void Delete(int id);
        OrderItem CreateOrderItem(OrderItemDTO orderItem);
        OrderItem UpdateOrderItem(OrderItemDTO orderItem);
    }
}
