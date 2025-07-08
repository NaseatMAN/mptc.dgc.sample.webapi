namespace mptc.dgc.sample.application.Exceptions
{
    public class AuthenticationException(string message,string errorCode = "InvalidToken") : Exception(message)
    {
        public string ErrorCode { get; } = errorCode;
    }
}
