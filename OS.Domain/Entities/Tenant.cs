namespace OS.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Subdomain { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public DateTime? PlanExpiryDate { get; set; }
    }
}
