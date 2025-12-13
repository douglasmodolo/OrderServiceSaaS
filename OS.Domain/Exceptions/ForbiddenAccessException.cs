namespace OS.Domain.Exceptions
{
    // Lançada quando o usuário logado tenta acessar dados de outro Tenant - Mapeia para 403 Forbidden
    public class ForbiddenAccessException : DomainException
    {
        public ForbiddenAccessException(string message) : base(message)
        {
        }

        public ForbiddenAccessException() : base("Access to the specified resource is forbidden.")
        {
        }
    }
}
