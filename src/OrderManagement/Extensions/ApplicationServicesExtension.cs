using Microsoft.EntityFrameworkCore;
using OrderManagement.Database;
using OrderManagement.Services;

namespace OrderManagement.Extensions;

public static class ApplicationServicesExtension
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrdersService, OrdersService>();
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}