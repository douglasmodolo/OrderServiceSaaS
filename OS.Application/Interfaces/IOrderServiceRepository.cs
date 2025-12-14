using OS.Domain.Entities;

namespace OS.Application.Interfaces
{
    public interface IOrderServiceRepository
    {
        Task<OrderService> AddAsync(OrderService order);
        Task<List<OrderService>> GetAllAsync();
        Task<OrderService> GetByIdAsync(Guid id);
    }
}