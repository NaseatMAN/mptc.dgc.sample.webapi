using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using mptc.dgc.sample.application.Helpers;

namespace mptc.dgc.sample.webapi.Filter;

public class ApiDeprecateActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var shouldStop = await ApiVersionDeprecationHelper.HandleApiVersionDeprecationAsync(context.HttpContext);
        if (shouldStop)
        {
            context.Result = new EmptyResult();
            return;
        }

        await next();
    }
}