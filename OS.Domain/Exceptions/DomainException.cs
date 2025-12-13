namespace OS.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }

        // Usada para retornar erros de validação
        public List<string> Errors { get; } = new List<string>();

        protected DomainException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
