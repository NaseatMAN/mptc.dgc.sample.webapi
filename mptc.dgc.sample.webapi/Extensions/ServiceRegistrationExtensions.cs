using mptc.dgc.sample.application.Interfaces.IUser;
using mptc.dgc.sample.application.Mappings;
using mptc.dgc.sample.application.Repositories;
using mptc.dgc.sample.webapi.Filter;

namespace mptc.dgc.sample.webapi.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<ApiDeprecateActionFilter>();
        return services;
    }
}