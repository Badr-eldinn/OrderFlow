using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.Queries.Dashboard;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly ISender _sender;

    public DashboardController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("orders")]
    public async Task<ActionResult<List<DashboardOrderDto>>> GetOrders(
        CancellationToken cancellationToken)
    {
        var query = new GetDashboardOrdersQuery();

        var orders = await _sender.Send(
            query,
            cancellationToken);

        return Ok(orders);
    }
}