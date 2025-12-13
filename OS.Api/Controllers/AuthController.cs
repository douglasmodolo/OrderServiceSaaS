using MediatR;
using Microsoft.AspNetCore.Mvc;
using OS.Application.Features.Auth.Commands;
using OS.Application.Features.Auth.Dtos;

namespace OS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gera um token JWT que contém o TenantId necessário para acessar dados.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Authenticate(AuthenticateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}