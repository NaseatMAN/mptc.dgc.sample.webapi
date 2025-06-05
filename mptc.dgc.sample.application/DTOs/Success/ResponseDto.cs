using mptc.dgc.sample.application.Constants;

namespace mptc.dgc.sample.application.DTOs.Success
{
    public class ResponseDto<T> 
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }  = string.Empty;
        public T? Data { get; set; }

        public static ResponseDto<T> Success(T? data, string message = "", int statusCode = StatusCodeConstants.Status200Ok) =>
            new()
            {
                StatusCode = statusCode,
                Data = data,
                Message = message
            };
    }
}
