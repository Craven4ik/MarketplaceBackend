using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceBackend.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "public");

        builder.Property(x => x.ID).ValueGeneratedOnAdd();

        builder.HasKey(c => c.ID);

        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId);

        builder.HasMany(c=> c.OrderItems)
            .WithOne()
            .HasForeignKey(c=> c.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


