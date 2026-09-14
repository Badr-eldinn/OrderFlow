using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(
        Order order,
        CancellationToken cancellationToken);

    Task<Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<List<Order>> GetAllAsync(
        CancellationToken cancellationToken);
}