using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Services;

public class OrderProcessingService : IOrderProcessingService
{
    private readonly OrderFlowDbContext _context;
    private readonly ICacheService _cacheService;

    public OrderProcessingService(
        OrderFlowDbContext context,
        ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task ProcessPendingOrdersAsync(
        CancellationToken cancellationToken)
    {
        var pendingOrders = await _context.Orders
            .Where(x => x.Status == "Pending")
            .ToListAsync(cancellationToken);

        foreach (var order in pendingOrders)
        {
            order.Complete();

            await _cacheService.RemoveAsync(
                $"order:{order.Id}");
        }

        if (pendingOrders.Count > 0)
        {
            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}