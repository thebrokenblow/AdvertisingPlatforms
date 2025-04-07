using AdvertisingPlatforms.WebApi.Configurations;
using AdvertisingPlatforms.WebApi.Middleware;
using AdvertisingPlatforms.WebApi.Model;
using AdvertisingPlatforms.WebApi.Model.Interfaces;
using AdvertisingPlatforms.WebApi.Model.Mappers;
using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;
using AdvertisingPlatforms.WebApi.Model.Validation;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdvertisingPlatforms.WebApi;

public partial class Startup(IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "AdvertisingPlatforms API", Version = "v1" });
        });

        services.AddSingleton<IAdvertisingRegion, AdvertisingRegion>();

        services.AddSingleton<IMapperLocationElements, MapperLocationElements>();
        services.AddSingleton<IMapperLocationAdvertisingPlatform, MapperLocationAdvertisingPlatform>();

        services.AddSingleton<ValidatorMapperLocationElements>();
        services.AddSingleton<ValidatorMapperLocationAdvertisingPlatform>();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdvertisingPlatforms API v1");
            c.RoutePrefix = string.Empty; // Устанавливаем пустой префикс маршрута для Swagger
        });


        app.UseCustomExceptionHandler();
        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}