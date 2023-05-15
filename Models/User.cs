using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Models;

public class User : IdentityUser
{
    //public string Email { get; set; }
    //public string UserName { get; set; }
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
