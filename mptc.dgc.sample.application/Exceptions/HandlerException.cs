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
                errorResponse = new ResponseErrorDto
                {
                    Error = new ErrorDto
                    {
                        Code = nfEx.ErrorCode,
                        Message = nfEx.Message,
                        Target = nfEx.TargetSite!.Name,
                        InnerError = new InnerErrorDto
                        {
                            Code = nfEx.ErrorCode,
                            Message = $"traceId: {context.TraceIdentifier}"
                        }
                    }
                };
                break;

            case BadRequestException brEx:
                statusCode = StatusCodes.Status400BadRequest;
                errorResponse = new ResponseErrorDto
                {
                    Error = new ErrorDto
                    {
                        Code = brEx.ErrorCode,
                        Message = brEx.Message,
                        Target = brEx.TargetSite!.Name,
                        InnerError = new InnerErrorDto
                        {
                            Code = brEx.ErrorCode,
                            Message = $"traceId: {brEx.InnerException?.Message}"
                        }
                    }
                };
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                errorResponse = new ResponseErrorDto
                {
                    Error = new ErrorDto
                    {
                        Code = "InternalServerError",
                        Message = "An unexpected error occurred.",
                        Target = exception.TargetSite!.Name,
                        InnerError = new InnerErrorDto
                        {
                            Code = "UnhandledException",
                            Message = $"traceId: {context.TraceIdentifier}"
                        }
                    }
                };
                break;
        }

        context.Response.StatusCode = statusCode;
        var json = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(json);
    }
}