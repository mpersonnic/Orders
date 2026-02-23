using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.API.Order;
using Orders.Application.Orders.Create;

namespace Orders.API.Controllers.Orders;

[ApiController]
[Route("api/orders")]
public class CreateOrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "CanWriteOrders")]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        // Récupération du CustomerId depuis le JWT
        var customerIdClaim = User.FindFirst("sub")?.Value; 
        if (customerIdClaim is null) 
            return Unauthorized("Missing 'sub' claim in token."); 
        var customerId = Guid.Parse(customerIdClaim);


        var command = new CreateOrderCommand(
            customerId,
            request.Items.Select(i => new CreateOrderItemDto(
                i.ProductName,
                i.Quantity,
                i.UnitPrice
            )).ToList()
        );
        // Accès aux claims de l'utilisateur authentifié :
        var userId = User.FindFirst("sub")?.Value; 
        var scopes = User.FindAll("scope").Select(c => c.Value).ToList();

        var orderId = await _mediator.Send(command);

        return Created($"/api/orders/{orderId}", null);
    }
}
