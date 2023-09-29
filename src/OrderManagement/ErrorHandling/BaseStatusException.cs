using OrderManagement.Models;

namespace OrderManagement.ErrorHandling;

#nullable enable
public abstract class BaseStatusException : Exception
{
    public abstract ErrorResponse GetError();

    protected BaseStatusException()
    {
    }

    protected BaseStatusException(string? message) : base(message)
    {
    }

    protected BaseStatusException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}

public record ErrorResponse
(
    ErrorCode ErrorCode,
    string ErrorMessage
)
{
    public bool HasError => ErrorCode != ErrorCode.NoError;
}