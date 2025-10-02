using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.LMS.Presentation.Exceptions
{
    internal sealed class GlobalExceptionHandler(
        IProblemDetailsService problemDetailService,
        ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailService = problemDetailService;
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occured.");

            httpContext.Response.StatusCode = exception switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return await _problemDetailService.TryWriteAsync(
                new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    Exception = exception,
                    ProblemDetails = new ProblemDetails
                    {
                        Type = exception.GetType().Name,
                        Title = "An error occured",
                        Detail = exception.Message
                    }
                });
        }
    }
}
