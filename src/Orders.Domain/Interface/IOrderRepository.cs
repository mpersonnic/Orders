using Orders.Domain.Orders;

namespace Orders.Domain.Interface
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(OrderId id, CancellationToken ct = default);
        Task AddAsync(Order order, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
