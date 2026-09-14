using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

namespace OrderFlow.Application.Queries.Dashboard;

public record GetDashboardOrdersQuery
    : IRequest<List<DashboardOrderDto>>;