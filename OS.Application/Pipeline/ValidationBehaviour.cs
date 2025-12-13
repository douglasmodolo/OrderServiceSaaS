using FluentValidation;
using MediatR;

namespace OS.Application.Pipeline
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    // Se houver falhas, lança a ValidationException que será mapeada para HTTP 400 pelo Middleware da API.
                    var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
                    throw new Domain.Exceptions.ValidationException("One or more validation errors occurred.", errorMessages);
                }
            }
            return await next();
        }
    }
}