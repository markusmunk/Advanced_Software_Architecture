using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Database;
using OrderManagement.Extensions;

namespace OrderManagement;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices(_configuration);

        var mvc = services.AddControllers().AddControllersAsServices();

        SetJsonApiSerializer(mvc);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"); });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        RunMigrations(app).GetAwaiter().GetResult();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private void SetJsonApiSerializer(IMvcBuilder mvc)
    {
        mvc.AddJsonOptions(o => { o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
    }

    private async Task RunMigrations(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        while (!await CanConnectToDatabaseAsync(context, logger, lifetime.ApplicationStopping))
        {
            if (lifetime.ApplicationStopping.IsCancellationRequested)
            {
                logger.LogInformation("Application stop requested. Stopping migration");
                return;
            }

            logger.LogWarning("Failed to connect to database, sleeping...");
            await Task.Delay(TimeSpan.FromSeconds(5), lifetime.ApplicationStopping);
        }

        await context.Database.MigrateAsync();
    }
    
    private async Task<bool> CanConnectToDatabaseAsync(DbContext context, ILogger logger,
        CancellationToken cancellationToken)
    {
        try
        {
            await context.Database.ExecuteSqlAsync($"select 1", cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Database connection failed");
            return false;
        }
    }
}