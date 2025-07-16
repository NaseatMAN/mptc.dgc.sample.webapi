namespace mptc.dgc.sample.webapi.SettingModel
{
    public class RedisSetting
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string QueueKey { get; set; } = "data-queue";
    }
}
