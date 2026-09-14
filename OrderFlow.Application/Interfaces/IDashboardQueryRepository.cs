using System;
using System.Collections.Generic;
using System.Text;

using OrderFlow.Application.Queries.Dashboard;

namespace OrderFlow.Application.Interfaces;

public interface IDashboardQueryRepository
{
    Task<List<DashboardOrderDto>> GetOrdersAsync(
        CancellationToken cancellationToken);
}

