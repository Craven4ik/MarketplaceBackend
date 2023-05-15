using MarketPlace.Models;

namespace MarketplaceBackend.Models;

public class Order
{
    public int ID { get; init; }
    public string UserId { get; set; }
    public StateOrder State { get; set; } = StateOrder.InProgress;
    public virtual User User { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}


