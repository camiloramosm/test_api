using PropertySystem.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace PropertySystem.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception ocurred {Message}", ex.Message);

            var exceptionDetails = GetExceptionDetails(ex);

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while processing your request.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path
            };

            if (exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = (int)problemDetails.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationException",
                "One or more validation errors occurred.",
                validationException.Message,
                validationException.Errors),
            ArgumentNullException ane => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ArgumentNullException",
                "A required argument was null.",
                ane.Message,
                null),
            ArgumentException ae => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ArgumentException",
                "An argument was invalid.",
                ae.Message,
                null),
            InvalidOperationException ioe => new ExceptionDetails(
                StatusCodes.Status409Conflict,
                "InvalidOperationException",
                "The operation is not valid due to the current state of the object.",
                ioe.Message,
                null),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "InternalServerError",
                "An unexpected error occurred.",
                exception.Message,
                null)
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);

}
