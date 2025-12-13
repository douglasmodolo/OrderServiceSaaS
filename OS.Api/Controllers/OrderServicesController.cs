using MediatR;
using Microsoft.AspNetCore.Mvc;
using OS.Application.Features.OrderServices.Commands;
using OS.Domain.Entities;

namespace OS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova Ordem de Serviço para o Tenant logado.
        /// </summary>
        /// <param name="command">Dados da Ordem de Serviço.</param>
        /// <returns>A Ordem de Serviço criada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderService), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(CreateOrderServiceCommand command)
        {
            OrderService result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Create), result);
        }

    }
}