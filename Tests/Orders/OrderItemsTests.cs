using FluentAssertions;
using Orders.Domain.Orders;

namespace Tests.Orders
{
    public class OrderItemTests
    {
        [Fact]
        public void Constructor_ShouldThrow_WhenQuantityIsZero()
        {
            Action act = () => new OrderItem(Guid.NewGuid(), "A", 0, 10m);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void IncreaseQuantity_ShouldAddAmount()
        {
            var item = new OrderItem(Guid.NewGuid(), "A", 2, 10m);

            item.IncreaseQuantity(3);

            item.Quantity.Should().Be(5);
        }
    }

    
}
