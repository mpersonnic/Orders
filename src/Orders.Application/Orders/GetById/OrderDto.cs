namespace Orders.Application.Orders.GetById;

public sealed record OrderDto(
    Guid OrderId,
    Guid CustomerId,
    bool IsPlaced,
    List<OrderItemDto> Items
);

public sealed record OrderItemDto(
    Guid Id,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Total
);
