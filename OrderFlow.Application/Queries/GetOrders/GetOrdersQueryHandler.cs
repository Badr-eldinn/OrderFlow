using System;
using System.Collections.Generic;
using System.Text;

using MediatR;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.Queries.GetOrders;

public class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQuery, List<OrderListDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderListDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(
            cancellationToken);

        return orders.Select(order => new OrderListDto(
            order.Id,
            order.CustomerName,
            order.Items.Count,
            order.Total,
            order.Status,
            order.CreatedAt
        )).ToList();
    }
}
