using Microsoft.AspNetCore.Mvc;
using Order_Management.Controllers.ResponseModels;

namespace Order_Management.Controllers;

[ApiController]
[Route("/api/orders")]
public class OrdersController: ControllerBase
{
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<OrderTo> GetOrders()
    {
        _logger.LogDebug("Fetching all orders");
        
        return new List<OrderTo>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Burger"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Fries"
            }
        };
    } 
}