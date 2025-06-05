using Microsoft.AspNetCore.Http;

namespace mptc.dgc.sample.application.Helpers
{
    public static class ApiVersionDeprecationHelper
    {
        private static readonly Dictionary<string, DateTime> DeprecatedVersions = new()
        {
            { "2025-06-01", new DateTime(2025, 6, 6,0, 0, 0,DateTimeKind.Utc) }
        };

        public static async Task<bool> HandleApiVersionDeprecationAsync(HttpContext context)
        {
            if (!context.Request.Query.TryGetValue("api-version", out var version)) return false;
            if (!DeprecatedVersions.TryGetValue(version!, out var sunsetDate)) return false;
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("Deprecation", "true");
                context.Response.Headers.Add("Sunset", sunsetDate.ToUniversalTime().ToString("R"));
                context.Response.Headers.Add("Link", $"</docs/{version}>; rel=\"deprecation\"");
                return Task.CompletedTask;
            });

            if (DateTime.UtcNow < sunsetDate) return false;
            context.Response.StatusCode = StatusCodes.Status410Gone;
            await context.Response.WriteAsync($"API version {version} is retired.");
            return true;

        }
    }
}
