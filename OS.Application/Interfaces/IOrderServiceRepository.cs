using OS.Domain.Entities;

namespace OS.Application.Interfaces
{
    public interface IOrderServiceRepository
    {
        Task<OrderService> AddAsync(OrderService order);
        // Outros métodos: GetByIdAsync, UpdateAsync, etc.
    }
}