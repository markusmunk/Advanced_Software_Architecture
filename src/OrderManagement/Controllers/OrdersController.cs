using Microsoft.AspNetCore.Mvc;
using OrderManagement.Controllers.RequestModels;
using OrderManagement.Controllers.ResponseModels;
using OrderManagement.Services;

namespace OrderManagement.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet]
    public async Task<List<OrderTo>> GetOrders(CancellationToken cancellationToken)
    {
        return await _ordersService.GetOrders(cancellationToken);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<OrderTo> GetOrder(Guid orderId, CancellationToken cancellationToken)
    {
        return await _ordersService.GetOrderById(orderId, cancellationToken);
    }

    [HttpPost]
    public async Task<OrderTo> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        return await _ordersService.CreateOrder(request, cancellationToken);
    }
}