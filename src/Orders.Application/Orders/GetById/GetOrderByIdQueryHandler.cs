using MediatR;
using Orders.Domain.Interface;
using Orders.Domain.Orders;

namespace Orders.Application.Orders.GetById;

public sealed class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orders;

    public GetOrderByIdQueryHandler(IOrderRepository orders)
    {
        _orders = orders;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken ct)
    {
        var order = await _orders.GetByIdAsync(new OrderId(request.OrderId), ct);

        if (order is null)
            return null;

        return new OrderDto(
            order.Id.Value,
            order.CustomerId,
            order.IsPlaced,
            order.Items.Select(i => new OrderItemDto(
                i.Id,
                i.ProductName,
                i.Quantity,
                i.UnitPrice,
                i.Total
            )).ToList()
        );
    }
}
