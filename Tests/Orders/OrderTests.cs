using Orders.Domain.Orders;
using FluentAssertions;
using Orders.Domain.Events;

namespace Tests.Orders
{


    public class OrderTests
    {
        [Fact]
        public void Create_ShouldInitializeOrderCorrectly()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act
            var order = Order.Create(customerId);

            // Assert
            order.CustomerId.Should().Be(customerId);
            order.IsPlaced.Should().BeFalse();
            order.Items.Should().BeEmpty();
            order.Id.Value.Should().NotBeEmpty();
        }


        [Fact]
        public void AddItem_ShouldAddNewItem()
        {
            var order = Order.Create(Guid.NewGuid());

            order.AddItem("Product A", 2, 10m);

            order.Items.Should().HaveCount(1);
            order.Items.First().ProductName.Should().Be("Product A");
            order.Items.First().Quantity.Should().Be(2);
        }
        [Fact]
        public void AddItem_ShouldIncreaseQuantity_WhenProductAlreadyExists()
        {
            var order = Order.Create(Guid.NewGuid());

            order.AddItem("Product A", 2, 10m);
            order.AddItem("Product A", 3, 10m);

            order.Items.Should().HaveCount(1);
            order.Items.First().Quantity.Should().Be(5);
        }


        [Fact]
        public void Total_ShouldReturnCorrectSum()
        {
            var order = Order.Create(Guid.NewGuid());

            order.AddItem("A", 2, 10m); // 20
            order.AddItem("B", 1, 5m);  // 5

            order.Total().Should().Be(25m);
        }

        [Fact]
        public void Place_ShouldMarkOrderAsPlaced_AndAddDomainEvent()
        {
            var order = Order.Create(Guid.NewGuid());
            order.AddItem("A", 1, 10m);

            order.Place();

            order.IsPlaced.Should().BeTrue();
            order.DomainEvents.Should().HaveCount(1);
            order.DomainEvents.First().Should().BeOfType<OrderPlaced>();
        }

        [Fact]
        public void Place_ShouldThrow_WhenOrderIsEmpty()
        {
            var order = Order.Create(Guid.NewGuid());

            Action act = () => order.Place();

            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Cannot place an empty order.");
        }
    }
}
