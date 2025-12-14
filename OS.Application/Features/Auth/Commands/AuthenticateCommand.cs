using MediatR;
using OS.Application.Features.Auth.Dtos;
using System.Security.Claims;
using OS.Domain.Exceptions;
using OS.Application.Interfaces.Services;

namespace OS.Application.Features.Auth.Commands
{
    public class AuthenticateCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, AuthResponse>
    {
        private readonly IJwtService _jwtService;

        public AuthenticateHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            if (request.Email != "test@tenantA.com" || request.Password != "P@ssword1")
            {
                throw new ForbiddenAccessException("Invalid credentials.");
            }

            var userId = "user-test-123";
            var tenantId = Guid.Parse("a1a1a1a1-aaaa-bbbb-cccc-123456789012");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim("tenantid", tenantId.ToString().ToLowerInvariant()),
            };

            var token = _jwtService.GenerateToken(claims);

            return new AuthResponse
            {
                Token = token,
                UserId = userId,
                TenantId = tenantId
            };
        }
    }
}