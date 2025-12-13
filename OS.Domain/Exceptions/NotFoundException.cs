namespace OS.Domain.Exceptions
{
    // Lançada quando um recurso não é encontrado - Mapeia para 404 Not Found
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
