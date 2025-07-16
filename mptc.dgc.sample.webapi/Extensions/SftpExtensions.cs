using mptc.dgc.sample.application.DTOs.File;

namespace mptc.dgc.sample.webapi.Extensions
{
    public static class SftpExtensions
    {
        public static IServiceCollection AddSftpConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var sftpConfig = new SftpConfig();
            configuration.GetSection("SftpConfig").Bind(sftpConfig);
            services.AddSingleton(sftpConfig);
            return services;
        }
    }
}
