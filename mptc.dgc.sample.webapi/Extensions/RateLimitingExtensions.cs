using System.Threading.RateLimiting;
using mptc.dgc.sample.application.Exceptions;

namespace mptc.dgc.sample.webapi.Extensions
{
    public static class RateLimitingExtensions
    {
        public static IServiceCollection AddUserBasedRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("UserBased", httpContext =>
                {
                    var ratLimit = configuration.GetSection("RateLimit");
                    var limitRequest = ratLimit["PermitLimit"];
                    var userId = httpContext.Items["RateLimitUserId"] as string ?? "anonymous";

                    return RateLimitPartition.GetFixedWindowLimiter(userId, _ =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = Convert.ToInt32(limitRequest),
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        });
                });
                options.OnRejected = (_, _) => throw new RateLimitRejectedException("TooManyRequest");
            });

            return services;
        }
    }
}
