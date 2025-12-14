using MediatR;
using OS.Application.Interfaces;
using OS.Domain.Entities;

namespace OS.Application.Features.OrderServices.Queries
{
    public class GetOrderServicesQuery : IRequest<List<OrderService>> {    }

    public class GetOrderServicesQueryHandler : IRequestHandler<GetOrderServicesQuery, List<OrderService>>
    {
        private readonly IOrderServiceRepository _repository;

        public GetOrderServicesQueryHandler(IOrderServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderService>> Handle(GetOrderServicesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
