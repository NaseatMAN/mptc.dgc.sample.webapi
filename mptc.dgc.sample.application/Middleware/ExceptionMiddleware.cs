using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using mptc.dgc.sample.application.Exceptions;
using Microsoft.ApplicationInsights;

namespace mptc.dgc.sample.application.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, TelemetryClient telemetry)
    {
        private readonly TelemetryClient _telemetry = telemetry;
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Items.ContainsKey("ExpiredToken"))
                {
                    throw new SecurityTokenExpiredException("Token Expired");
                }
                if (context.Items.ContainsKey("InvalidToken"))
                {
                    throw new AuthenticationException("Token Expired");
                }
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                _telemetry.TrackException(ex, new Dictionary<string, string>
                {
                    { "Path", context.Request.Path },
                    { "TraceId", context.TraceIdentifier },
                    { "Method", context.Request.Method },
                    { "User", context.User?.Identity?.Name ?? "Anonymous" }
                });
                await HandlerException.HandleExceptionAsync(context, ex);
            }
        }
    }
}
