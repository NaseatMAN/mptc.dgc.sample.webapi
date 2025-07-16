namespace mptc.dgc.sample.application.Interfaces
{
    public interface IRedisQueueService
    {
        Task EnqueueAsync<T>(T item);
        Task<T?> DequeueAsync<T>();
        Task<T?> PeekAsync<T>();
        Task<long> GetQueueLengthAsync();
        Task ClearQueueAsync();
    }
}
