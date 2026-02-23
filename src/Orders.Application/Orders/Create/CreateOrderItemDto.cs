namespace Orders.Application.Orders.Create
{
    public sealed record CreateOrderItemDto(
        string ProductName,
        int Quantity,
        decimal UnitPrice
    );

}
