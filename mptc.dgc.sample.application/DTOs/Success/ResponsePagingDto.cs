namespace mptc.dgc.sample.application.DTOs.Success
{
    public class ResponsePagingDto<T>
    {
        public int TotalCount { get; set; }
        public string? NextLink { get; set; }
        public List<T> Value { get; set; } = new List<T>();

    }
}
