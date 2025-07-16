using Microsoft.Extensions.Options;
using mptc.dgc.sample.application.Interfaces;
using System.Text.Json;
using mptc.dgc.sample.application.DTOs;
using StackExchange.Redis;

namespace mptc.dgc.sample.application.Services
{
    public class RedisQueueService(IConnectionMultiplexer redis, IOptions<RedisSetting> redisOptions)
        : IRedisQueueService
    {
        private readonly IDatabase _db = redis.GetDatabase();
        private readonly string _queueKey = redisOptions.Value.QueueKey ?? throw new ArgumentException("QueueKey is required in Redis settings.");
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        public async Task EnqueueAsync<T>(T item)
        {
            var json = JsonSerializer.Serialize(item, _jsonOptions);
            await _db.ListRightPushAsync(_queueKey, json);
        }

        public async Task<T?> DequeueAsync<T>()
        {
            var value = await _db.ListLeftPopAsync(_queueKey);
            return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value!, _jsonOptions);
        }

        public async Task<T?> PeekAsync<T>()
        {
            var value = await _db.ListGetByIndexAsync(_queueKey, 0);
            return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value!, _jsonOptions);
        }

        public async Task<long> GetQueueLengthAsync()
        {
            return await _db.ListLengthAsync(_queueKey);
        }

        public async Task ClearQueueAsync()
        {
            await _db.KeyDeleteAsync(_queueKey);
        }
    }
}
