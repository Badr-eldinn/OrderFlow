using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

namespace OrderFlow.Application.Queries.GetOrders;

public record GetOrdersQuery : IRequest<List<OrderListDto>>;

public record OrderListDto(
    Guid Id,
    string CustomerName,
    int ItemCount,
    decimal Total,
    string Status,
    DateTime CreatedAt
);