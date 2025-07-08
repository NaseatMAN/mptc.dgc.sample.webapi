using Microsoft.AspNetCore.Http;
using mptc.dgc.sample.application.DTOs.Error;
using System.Text.Json;

namespace mptc.dgc.sample.application.Exceptions;

public static class HandlerException
{
    public static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ResponseErrorDto errorResponse;
        int statusCode;

        switch (exception)
        {
            case NotFoundException nfEx:
                statusCode = StatusCodes.Status404NotFound;
                errorResponse = CreateErrorDto(nfEx.ErrorCode, nfEx.Message, nfEx.TargetSite?.Name, context.TraceIdentifier);
                break;

            case BadRequestException brEx:
                statusCode = StatusCodes.Status400BadRequest;
                errorResponse = CreateErrorDto(brEx.ErrorCode, brEx.Message, brEx.TargetSite?.Name, context.TraceIdentifier, brEx.InnerException?.Message);
                break;

            case SecurityTokenExpiredException steEx:
                statusCode = StatusCodes.Status401Unauthorized;
                errorResponse = CreateErrorDto(steEx.ErrorCode, steEx.Message, steEx.TargetSite?.Name, context.TraceIdentifier);
                break;

            case RateLimitRejectedException rlEx:
                statusCode = StatusCodes.Status429TooManyRequests;
                errorResponse = CreateErrorDto(rlEx.ErrorCode, "Too many requests. Please try again later.", rlEx.TargetSite?.Name, context.TraceIdentifier);
                break;

            case AuthenticationException authEx:
                statusCode = StatusCodes.Status401Unauthorized;
                errorResponse = CreateErrorDto(authEx.ErrorCode, authEx.Message, authEx.TargetSite?.Name, context.TraceIdentifier);
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                errorResponse = CreateErrorDto("InternalServerError", "An unexpected error occurred.", exception.TargetSite?.Name, context.TraceIdentifier);
                break;
        }

        context.Response.StatusCode = statusCode;
        var json = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(json);
    }

    private static ResponseErrorDto CreateErrorDto(string code, string message, string? target, string traceId, string? innerMessage = null)
    {
        return new ResponseErrorDto
        {
            Error = new ErrorDto
            {
                Code = code,
                Message = message,
                Target = target ?? "Unknown",
                InnerError = new InnerErrorDto
                {
                    Code = code,
                    Message = $"traceId: {traceId}" + (innerMessage != null ? $" | {innerMessage}" : "")
                }
            }
        };
    }
}