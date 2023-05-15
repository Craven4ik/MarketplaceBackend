using MarketplaceBackend.Models;

namespace MarketplaceBackend.DTO;

public class OrderFilter
{
    public Guid? UserID { get; init; }
    public StateOrder? State { get; init; }
}
