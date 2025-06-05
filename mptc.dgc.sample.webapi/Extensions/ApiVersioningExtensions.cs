
using Asp.Versioning;

namespace mptc.dgc.sample.webapi.Extensions;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = false;
            options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            options.ReportApiVersions = true;
            options.UnsupportedApiVersionStatusCode = StatusCodes.Status400BadRequest;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "yyyy-MM-dd";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}