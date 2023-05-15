using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceBackend.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", "public");

            builder.Property(x => x.ID).ValueGeneratedOnAdd();

            builder.HasKey(c => c.ID);

            builder.HasOne(c => c.Order)
                .WithMany()
                .HasForeignKey(c => c.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductID);
        }
    }
}
