using OrderManagement.ErrorHandling;
using OrderManagement.Models;

namespace OrderManagement.Controllers.Exceptions;

public class OrderNotFoundException: NotFoundException
{
    public OrderNotFoundException(string message = "Could not find collection") : base(message)
    {
        
    }

    public override ErrorResponse GetError()
    {
        return new(ErrorCode.NotFound, Message);
    }
}