using OS.Application.Interfaces;
using OS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OS.Infrastructure.Persistence
{
    public class OrderServiceRepository : IOrderServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderService> AddAsync(OrderService order)
        {
            await _context.OrderServices.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<OrderService>> GetAllAsync()
        {
            return await _context.OrderServices
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OrderService?> GetByIdAsync(Guid id)
        {
            return await _context.OrderServices
                .AsNoTracking()
                .FirstOrDefaultAsync(os => os.Id == id);
        }
    }
}