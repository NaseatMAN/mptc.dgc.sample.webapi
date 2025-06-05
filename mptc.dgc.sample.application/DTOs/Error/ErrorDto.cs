namespace mptc.dgc.sample.application.DTOs.Error
{
    public class ErrorDto
    {
        public string Code { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<ErrorDetailDto> Details { get; set; } = new List<ErrorDetailDto>();
        public InnerErrorDto? InnerError { get; set; }
    }
}
