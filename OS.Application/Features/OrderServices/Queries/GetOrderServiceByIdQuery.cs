using MediatR;
using OS.Application.Interfaces;
using OS.Domain.Entities;

namespace OS.Application.Features.OrderServices.Queries
{
    public class GetOrderServiceByIdQuery : IRequest<OrderService>
    {
        public Guid Id { get; set; }
    }

    public class GetOrderServiceByIdQueryHandler : IRequestHandler<GetOrderServiceByIdQuery, OrderService>
    {
        private readonly IOrderServiceRepository _repository;
        
        public GetOrderServiceByIdQueryHandler(IOrderServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderService> Handle(GetOrderServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);

            if (order == null)
            {
                throw new KeyNotFoundException($"OrderService with Id {request.Id} not found.");
            }

            return order;
        }
    }

}
