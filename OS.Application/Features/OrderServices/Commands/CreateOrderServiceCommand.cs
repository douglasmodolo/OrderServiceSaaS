using MediatR;
using OS.Application.Interfaces.Repositories;
using OS.Domain.Entities;

namespace OS.Application.Features.OrderServices.Commands
{
    public class CreateOrderServiceCommand : IRequest<OrderService>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ClientId { get; set; }
    }

    public class CreateOrderServiceHandler : IRequestHandler<CreateOrderServiceCommand, OrderService>
    {
        private readonly IOrderServiceRepository _repository;

        public CreateOrderServiceHandler(IOrderServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderService> Handle(CreateOrderServiceCommand request, CancellationToken cancellationToken)
        {
            var newOrder = new OrderService
            {
                Title = request.Title,
                Description = request.Description,
                ClientId = request.ClientId
            };

            var createdOrder = await _repository.AddAsync(newOrder);

            return createdOrder;
        }
    }
}