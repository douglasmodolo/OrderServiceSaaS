using OS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace OS.Infrastructure.Services
{
    public class TenantContext : ITenantContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? TenantId
        {
            get
            {
                var tenantIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("tenantid");

                if (tenantIdClaim != null && Guid.TryParse(tenantIdClaim.Value, out var tenantId))
                {
                    return tenantId;
                }
                return null;
            }
        }

        public bool IsTenantContextActive => TenantId.HasValue;
    }
}