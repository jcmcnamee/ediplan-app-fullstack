using Ediplan.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Text.Json;

namespace Ediplan.Api.Middleware;
public class ExceptionHandlerMiddleware
{
    // The next middleware in the pipeline
    private readonly RequestDelegate _next;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ExceptionHandlerMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
    {
        _next = next;
        _problemDetailsFactory = problemDetailsFactory;
    }

    // This method catches any exceptions
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                httpStatusCode = HttpStatusCode.UnprocessableContent;
                result = BuildValidationProblemDetails(
                    context,
                    httpStatusCode,
                    validationException
                    );
                break;
            case BadRequestException badRequestException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = badRequestException.Message;
                break;
            case NotFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case Exception:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }

    private string BuildValidationProblemDetails(HttpContext context, HttpStatusCode httpStatusCode, ValidationException validationException)
    {
        string result;
        var modelState = new ModelStateDictionary();
        foreach (var error in validationException.ValidationErrors)
        {
            modelState.AddModelError(string.Empty, error);
        }

        result = JsonSerializer.Serialize(_problemDetailsFactory.CreateValidationProblemDetails(
            context,
            modelState,
            statusCode: (int)httpStatusCode)
            );

        return result;
    }
}

