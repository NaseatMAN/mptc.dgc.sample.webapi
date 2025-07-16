using StackExchange.Redis;

namespace mptc.dgc.sample.webapi.Extensions
{
    public static class RedisExtension
    {
        public static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSetting = new SettingModel.RedisSetting();
            configuration.GetSection("Redis").Bind(redisSetting);
            services.AddSingleton<IConnectionMultiplexer>(_ =>
            {
                var connection = ConnectionMultiplexer.Connect(redisSetting.ConnectionString);
                return connection;
            });
            return services;
        }
    }
}
