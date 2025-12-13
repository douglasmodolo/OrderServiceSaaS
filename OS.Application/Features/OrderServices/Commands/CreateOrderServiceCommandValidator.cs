using FluentValidation;
using OS.Domain.Interfaces;
using OS.Domain.Exceptions;

namespace OS.Application.Features.OrderServices.Commands
{
    public class CreateOrderServiceCommandValidator : AbstractValidator<CreateOrderServiceCommand>
    {
        private readonly ITenantContext _tenantContext;

        public CreateOrderServiceCommandValidator(ITenantContext tenantContext)
        {
            _tenantContext = tenantContext;

            RuleFor(v => v)
                .Must(HaveActiveTenantContext)
                .WithMessage("Unauthorized: Cannot create Order Service without an active Tenant context.");

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(v => v.ClientId)
                .NotEmpty().WithMessage("Client ID is required.");
        }

        private bool HaveActiveTenantContext(CreateOrderServiceCommand command)
        {
            if (!_tenantContext.TenantId.HasValue)
            {
                // Poderíamos lançar uma ForbiddenAccessException aqui,
                // mas lançar uma exceção dentro de um validador FluentValidation
                // é geralmente evitado. O Must() deve retornar false para que o 
                // pipeline lance a ValidationException, que será mapeada para 400.

                // Para uma segurança mais rigorosa, podemos usar o ForbiddenAccessException 
                // no Handler se a validação passar e, por algum motivo, o TenantId sumir.

                // Por enquanto, apenas retornamos false para a validação.
                return false;
            }
            
            return true;
        }
    }
}