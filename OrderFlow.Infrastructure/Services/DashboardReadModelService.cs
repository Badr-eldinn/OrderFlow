using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Infrastructure.Data;
using OrderFlow.Infrastructure.Data.ReadModels;

namespace OrderFlow.Infrastructure.Services;

public class DashboardReadModelService
    : IDashboardReadModelService
{
    private readonly OrderFlowDbContext _context;

    public DashboardReadModelService(
        OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task RefreshAsync(
        CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var readModels = orders.Select(order =>
            new DashboardOrderReadModel
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                ItemCount = order.Items.Count,
                Total = order.Total,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            }).ToList();

        await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM DashboardOrders",
            cancellationToken);

        await _context.DashboardOrders.AddRangeAsync(
            readModels,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);
    }
}
