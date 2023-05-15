using MarketPlace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceBackend.Config;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.User)
            .WithMany(c => c.Items)
            //.HasForeignKey(p => p.UserId);
            .HasPrincipalKey(x => x.Id);
    }
}
