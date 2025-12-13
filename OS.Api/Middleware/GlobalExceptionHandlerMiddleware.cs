using OS.Domain.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace OS.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during request processing.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";
            var errorDetails = new ErrorDetails { Message = message, StatusCode = (int)statusCode };

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    message = "Validation failed.";
                    errorDetails.Errors.AddRange(validationException.Errors);
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404
                    message = exception.Message;
                    break;
                case ForbiddenAccessException:
                    statusCode = HttpStatusCode.Forbidden; // 403 - Acesso Negado ao recurso (dados de outro Tenant)
                    message = exception.Message;
                    break;
                case DomainException domainException:
                    // Outras exceções de domínio/negócio
                    statusCode = HttpStatusCode.BadRequest; // 400
                    message = domainException.Message;
                    break;
            }

            errorDetails.StatusCode = (int)statusCode;
            if (errorDetails.Message == "An unexpected error occurred.")
            {
                errorDetails.Message = message;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}