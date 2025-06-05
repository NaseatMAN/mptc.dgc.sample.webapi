using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using mptc.dgc.sample.application.DTOs.Error;

namespace mptc.dgc.sample.webapi.Extensions;

public static class InvalidModelStateResponse
{
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var errors = context.ModelState
            .Where(ms => ms.Value?.ValidationState == ModelValidationState.Invalid)
            .SelectMany(kvp => kvp.Value!.Errors.Select(e => new ValidationError
            {
                Field = kvp.Key,
                Message = e.ErrorMessage
            }))
            .ToList();

        var response = new ResponseErrorDto
        {
            Error = new ErrorDto
            {
                Code = "InvalidModelState",
                Message = "Validation failed for one or more fields.",
                Target = "ModelValidation",
                Details = errors.Select(e => new ErrorDetailDto
                {
                    Code = "ValidationError",
                    Message = e.Message,
                    Target = e.Field
                }).ToList()
            }
        };

        return new BadRequestObjectResult(response);
    }
}
public class ValidationError
{
    public string Field { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}