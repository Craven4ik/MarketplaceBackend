using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Models;

public class User : IdentityUser
{
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
