namespace mptc.dgc.sample.application.Exceptions;

public class BadRequestException(string message, string errorCode = "BadRequest") : Exception(message)
{
    public string ErrorCode { get; } = errorCode;
}