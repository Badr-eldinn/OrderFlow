using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

namespace OrderFlow.Application.Commands.CreateOrder;

public record CreateOrderCommand(
    string CustomerName,
    List<CreateOrderItemCommand> Items
) : IRequest<Guid>;

public record CreateOrderItemCommand(
    string ProductName,
    int Quantity,
    decimal UnitPrice
);