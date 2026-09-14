using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.Commands.CreateOrder;
using OrderFlow.Application.Queries.GetOrderById;
using OrderFlow.Application.Queries.GetOrders;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var orderId = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = orderId },
            orderId);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderListDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var query = new GetOrdersQuery();

        var orders = await _sender.Send(
            query,
            cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDetailsDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(id);

        var order = await _sender.Send(
            query,
            cancellationToken);

        return Ok(order);
    }
}