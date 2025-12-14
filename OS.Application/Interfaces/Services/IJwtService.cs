using System.Security.Claims;

namespace OS.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
