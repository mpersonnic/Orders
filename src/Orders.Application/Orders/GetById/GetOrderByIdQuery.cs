using MediatR;

namespace Orders.Application.Orders.GetById;

public sealed record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto?>;

