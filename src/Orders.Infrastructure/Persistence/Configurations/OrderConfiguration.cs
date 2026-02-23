using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Orders;

namespace Orders.Infrastructure.Persistence.Configurations
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.CustomerId)
                   .IsRequired();

            // Relation 1-N avec OrderItem
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey("OrderId")
                   .IsRequired();

            // Indique à EF Core d'utiliser le champ privé _items
            builder.Navigation(o => o.Items)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
