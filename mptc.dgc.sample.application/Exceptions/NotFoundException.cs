namespace mptc.dgc.sample.application.Exceptions;

public class NotFoundException(string message, string errorCode = "ResourceNotFound") : Exception(message)
{
    public string ErrorCode { get; } = errorCode;
}