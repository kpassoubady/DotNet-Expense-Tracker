using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Middleware;

/// <summary>
/// Middleware for handling unhandled exceptions and converting them to appropriate HTTP responses.
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware to handle the HTTP request.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="next">The next middleware in the pipeline.</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing request {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles the exception and writes an appropriate response.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = MapExceptionToStatusCode(exception);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = context.Request.Path,
            Type = $"https://httpstatuses.com/{statusCode}"
        };

        // Add validation errors for FluentValidation exceptions
        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(problemDetails, options);
        await context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Maps exception types to HTTP status codes.
    /// </summary>
    /// <param name="exception">The exception to map.</param>
    /// <returns>A tuple containing the status code and title.</returns>
    private static (int StatusCode, string Title) MapExceptionToStatusCode(Exception exception)
    {
        return exception switch
        {
            ValidationException => ((int)HttpStatusCode.BadRequest, "Validation Error"),
            KeyNotFoundException => ((int)HttpStatusCode.NotFound, "Resource Not Found"),
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Unauthorized Access"),
            InvalidOperationException => ((int)HttpStatusCode.BadRequest, "Invalid Operation"),
            ArgumentNullException => ((int)HttpStatusCode.BadRequest, "Invalid Argument"),
            ArgumentException => ((int)HttpStatusCode.BadRequest, "Invalid Argument"),
            _ => ((int)HttpStatusCode.InternalServerError, "Internal Server Error")
        };
    }
}
