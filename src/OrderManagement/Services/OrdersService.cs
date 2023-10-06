
using Microsoft.EntityFrameworkCore;
using OrderManagement.Controllers.Exceptions;
using OrderManagement.Controllers.RequestModels;
using OrderManagement.Controllers.ResponseModels;
using OrderManagement.Database;
using OrderManagement.Database.Models;

namespace OrderManagement.Services;

public interface IOrdersService
{
    Task<List<OrderTo>> GetOrders(CancellationToken cancellationToken);
    Task<OrderTo> GetOrderById(Guid id, CancellationToken cancellationToken);
    Task<OrderTo> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken);
}

public class OrdersService : IOrdersService
{
    private readonly ILogger<OrdersService> _logger;
    private readonly AppDbContext _context;

    public OrdersService(ILogger<OrdersService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<OrderTo>> GetOrders(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Fetching orders from database");

        var orders = await _context.Orders.ToListAsync(cancellationToken);

        _logger.LogDebug("Fetched orders from database");

        return orders.Select(BuildOrderTo).ToList();
    }

    public async Task<OrderTo> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(order => order.OrderId == id, cancellationToken);

        if (order == null)
        {
            throw new OrderNotFoundException();
        }

        return BuildOrderTo(order);
    }

    public async Task<OrderTo> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Order()
        {
            OrderId = Guid.NewGuid(),
            Name = request.Name
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return BuildOrderTo(order);
    }

    private OrderTo BuildOrderTo(Order order)
    {
        return new OrderTo()
        {
            Name = order.Name,
            Id = order.OrderId
        };
    }
}