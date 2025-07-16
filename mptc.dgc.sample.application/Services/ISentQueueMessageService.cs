namespace mptc.dgc.sample.application.Services
{
    public interface ISentQueueMessageService
    {
        Task SendMessageAsync<T>(T message);
    }
}
