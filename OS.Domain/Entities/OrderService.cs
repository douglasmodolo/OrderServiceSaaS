using OS.Domain.Interfaces;

namespace OS.Domain.Entities
{
    public class OrderService : BaseEntity, IMustHaveTenant
    {
        public Guid TenantId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";

        public Guid ClientId { get; set; }
    }
}
