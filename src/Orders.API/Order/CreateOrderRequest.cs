namespace Orders.API.Order
{
    public sealed record CreateOrderRequest(List<CreateOrderItemRequest> Items); 
    public sealed record CreateOrderItemRequest(
        string ProductName, 
        int Quantity, 
        decimal UnitPrice
        );
}
