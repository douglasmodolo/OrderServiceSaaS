namespace OS.Application.Features.Auth.Dtos
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
    }
}
