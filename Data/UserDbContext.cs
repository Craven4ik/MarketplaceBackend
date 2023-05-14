using MarketPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options, ModelBuilder modelBuilder)
            : base(options)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            //Database.EnsureCreated();
        }

        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
