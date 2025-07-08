namespace mptc.dgc.sample.application.Exceptions
{
    public class SecurityTokenExpiredException(string message,string errorCode = "TokenExpired") : Exception(message)
    {
        public string ErrorCode { get; } = errorCode;
    }
}
