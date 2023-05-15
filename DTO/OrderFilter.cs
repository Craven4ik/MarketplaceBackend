using MarketplaceBackend.Models;

namespace MarketplaceBackend.DTO;

public class OrderFilter
{
    public string? UserID { get; init; }
    public StateOrder? State { get; init; }
}
