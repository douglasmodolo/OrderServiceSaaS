using OS.Application.Interfaces.Repositories;
using OS.Domain.Entities;

namespace OS.Infrastructure.Persistence.Repositories
{
    public class OrderServiceRepository : GenericRepository<OrderService>, IOrderServiceRepository
    {
        public OrderServiceRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}