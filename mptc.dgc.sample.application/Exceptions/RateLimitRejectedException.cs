namespace mptc.dgc.sample.application.Exceptions
{
    public class RateLimitRejectedException(string message, string errorCode = "TooManyRequest") : Exception(message)
    {
        public string ErrorCode { get; } = errorCode;
    }
}
