namespace MarketplaceBackend.DTO
{
    public class OrderItemDTO
    {
        public int ID { get; init; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
    }
}
