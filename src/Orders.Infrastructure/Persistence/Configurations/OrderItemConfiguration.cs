using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Orders;

namespace Orders.Infrastructure.Persistence.Configurations
{

    public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductName).IsRequired();
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.UnitPrice).IsRequired();
        }
    }
}
