namespace mptc.dgc.sample.application.DTOs
{
    public class RedisSetting
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string QueueKey { get; set; } = "data-queue";
    }
}
