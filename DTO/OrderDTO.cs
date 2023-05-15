using MarketPlace.Models;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.DTO
{
    public class OrderDTO
    {
        public int ID { get; init; }
        public string UserID { get; set; }
        public StateOrder State { get; set; } = StateOrder.InProgress;
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
