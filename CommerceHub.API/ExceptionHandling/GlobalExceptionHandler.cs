using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace CommerceHub.API.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log the error for debugging purposes
            _logger.LogError(exception, "Exception caught by global handler: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path
            };

            // Map the exception type to the appropriate HTTP status code
            switch (exception)
            {
                // 400 Bad Request - FluentValidation
                case ValidationException validationException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Validation Failed";
                    problemDetails.Detail = "One or more validation errors occurred.";

                    // Group the errors by property name for clean JSON formatting
                    problemDetails.Extensions["errors"] = validationException.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray()
                        );
                    break;

                // 404 Not Found - Missing Resources
                // (You can throw a KeyNotFoundException in your queries when a DB record doesn't exist)
                case KeyNotFoundException notFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Resource Not Found";
                    problemDetails.Detail = notFoundException.Message;
                    break;

                // 500 Internal Server Error - Everything else
                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Detail = "An unexpected error occurred processing your request.";
                    break;
            }

            context.Response.StatusCode = problemDetails.Status.Value;

            // Write the ProblemDetails object as the JSON response
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            // Return true to indicate the exception has been completely handled
            return true;
        }
    }
}
