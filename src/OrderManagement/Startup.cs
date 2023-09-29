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

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context == null)
        {
            return;
        }
        
        var canConnect = await context.Database.CanConnectAsync();
        if (!canConnect)
        {
            return;
        }

        await context.Database.MigrateAsync();
    }
}