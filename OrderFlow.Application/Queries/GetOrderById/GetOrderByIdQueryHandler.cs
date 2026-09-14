using MediatR;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.Queries.GetOrderById;

public class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, OrderDetailsDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICacheService _cacheService;

    public GetOrderByIdQueryHandler(
        IOrderRepository orderRepository,
        ICacheService cacheService)
    {
        _orderRepository = orderRepository;
        _cacheService = cacheService;
    }

    public async Task<OrderDetailsDto> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"order:{request.Id}";

        // 1. Try to get order from Redis
        var cachedOrder =
            await _cacheService.GetAsync<OrderDetailsDto>(cacheKey);

        if (cachedOrder is not null)
        {
            Console.WriteLine("🔥 ORDER CAME FROM REDIS");
            return cachedOrder;
        }

        Console.WriteLine("🗄️ ORDER CAME FROM SQL SERVER");

        // 2. Get order from SQL Server
        var order = await _orderRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        // 3. Map Entity -> DTO
        var orderDto = new OrderDetailsDto(
            order.Id,
            order.CustomerName,
            order.Total,
            order.Status,
            order.CreatedAt,
            order.Items.Select(item => new OrderItemDto(
                item.Id,
                item.ProductName,
                item.Quantity,
                item.UnitPrice
            )).ToList()
        );

        // 4. Save result in Redis for 5 minutes
        await _cacheService.SetAsync(
            cacheKey,
            orderDto,
            TimeSpan.FromMinutes(5));

        // 5. Return result
        return orderDto;
    }
}