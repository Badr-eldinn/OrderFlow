using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Application.Queries.Dashboard;

public record DashboardOrderDto(
    Guid OrderId,
    string CustomerName,
    int ItemCount,
    decimal Total,
    string Status,
    DateTime CreatedAt
);
