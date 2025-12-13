namespace OS.Domain.Exceptions
{
    // Lançada quando a validação de dados falha - Mapeia para 400 Bad Request
    public class ValidationException : DomainException
    {
        public ValidationException(string message, List<string> errors) : base(message, errors)
        {
        }

        public ValidationException(string message) : base(message)
        {
        }
    }
}
