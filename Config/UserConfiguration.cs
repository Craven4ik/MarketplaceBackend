using MarketPlace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceBackend.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(c => c.Items)
                .WithOne(c => c.User)
                .HasForeignKey(p => p.OwnerEmail)
                .HasPrincipalKey(p => p.Email);
        }
    }
}
