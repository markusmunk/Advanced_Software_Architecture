using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Order_Management.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Order_Management;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();

        var mvc = services.AddControllers(ConfigureControllers).AddControllersAsServices();

        SetJsonApiSerializer(mvc);

        services.AddSwaggerGen(SetupSwagger);

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:5173");
            });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management"));

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    protected virtual void SetupSwagger(SwaggerGenOptions c)
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Management", Version = "v1" });
    }
    
    protected virtual void ConfigureControllers(MvcOptions options)
    {
    }

    protected virtual void SetJsonApiSerializer(IMvcBuilder mvc)
    {
        mvc.AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}