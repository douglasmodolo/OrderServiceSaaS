namespace OS.Domain.Interfaces
{
    public interface ITenantContext
    {
        Guid? TenantId { get; }
        bool IsTenantContextActive => TenantId.HasValue;
    }
}
