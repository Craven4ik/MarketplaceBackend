using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
