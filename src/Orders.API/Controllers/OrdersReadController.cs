using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Orders.GetById;

namespace Orders.API.Controllers.Orders;

[ApiController]
[Route("api/orders")]
[Authorize(Policy = "CanReadOrders")]
public class OrdersReadController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersReadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));

        return result is null ? NotFound() : Ok(result);
    }
}
