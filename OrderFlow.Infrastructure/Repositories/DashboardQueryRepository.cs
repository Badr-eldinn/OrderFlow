using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Interfaces;
using OrderFlow.Application.Queries.Dashboard;
using OrderFlow.Infrastructure.Data;

namespace OrderFlow.Infrastructure.Repositories;

public class DashboardQueryRepository : IDashboardQueryRepository
{
    private readonly OrderFlowDbContext _context;

    public DashboardQueryRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<DashboardOrderDto>> GetOrdersAsync(
        CancellationToken cancellationToken)
    {
        return await _context.DashboardOrders
            .AsNoTracking()
            .Select(order => new DashboardOrderDto(
                order.OrderId,
                order.CustomerName,
                order.ItemCount,
                order.Total,
                order.Status,
                order.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}