namespace mptc.dgc.sample.webapi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen();
            services.AddSwaggerConfiguration();
            services.AddHttpContextAccessor();
            services.AddAppDbContext(configuration);
            services.AddRedisConfiguration(configuration);
            services.AddAppServices();
            services.AddJwtAuthentication(configuration);
            services.AddUserBasedRateLimiting(configuration);
            services.AddSftpConfig(configuration);
            return services;
        }
    }
}
