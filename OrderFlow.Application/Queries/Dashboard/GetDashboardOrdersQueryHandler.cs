using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.Queries.Dashboard;

public class GetDashboardOrdersQueryHandler
    : IRequestHandler<
        GetDashboardOrdersQuery,
        List<DashboardOrderDto>>
{
    private readonly IDashboardQueryRepository _repository;

    public GetDashboardOrdersQueryHandler(
        IDashboardQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DashboardOrderDto>> Handle(
        GetDashboardOrdersQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetOrdersAsync(
            cancellationToken);
    }
}