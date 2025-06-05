using Microsoft.EntityFrameworkCore;
using mptc.dgc.sample.infrastructure.Models;

namespace mptc.dgc.sample.webapi.Extensions;

public static class DatabaseConnectionExtensions
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<SampleContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}