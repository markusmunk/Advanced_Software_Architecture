using OrderManagement.Models;

namespace OrderManagement.ErrorHandling;

#nullable enable
public abstract class NotFoundException : BaseStatusException
{
    protected NotFoundException()
    {
    }

    protected NotFoundException(string? message) : base(message)
    {
    }

    public override ErrorResponse GetError()
    {
        return new ErrorResponse(ErrorCode.NotFound, Message);
    }
}

public class IdNotFoundException : NotFoundException
{
    private readonly int _id;

    public IdNotFoundException(int id) : base($"Item with id {id} was not found")
    {
        _id = id;
    }

    public override ErrorResponse GetError()
    {
        return new IdNotFoundErrorResponse(_id)
        {
            ErrorCode = ErrorCode.NotFound,
            ErrorMessage = Message
        };
    }

    private record IdNotFoundErrorResponse(int Id) : ErrorResponse(ErrorCode.NotFound,
        $"Item with id {Id} was not found");
}