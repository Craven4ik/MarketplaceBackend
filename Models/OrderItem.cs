using MarketPlace.Models;

namespace MarketplaceBackend.Models
{
    public class OrderItem
    {
        public int ID { get; init; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public virtual Order Order { get; set; }
        public virtual Item Product { get; set; }
    }
}
