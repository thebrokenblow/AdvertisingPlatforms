using AdvertisingPlatforms.WebApi.Configurations;
using AdvertisingPlatforms.WebApi.Middleware;
using AdvertisingPlatforms.WebApi.Model;
using AdvertisingPlatforms.WebApi.Model.Interfaces;
using AdvertisingPlatforms.WebApi.Model.Mappers;
using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AdvertisingPlatforms.WebApi;

public partial class Startup(IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddScoped<IAdvertisingRegion, AdvertisingRegion>();
        services.AddScoped<IMapperLocationElements, MapperLocationElements>();
        services.AddScoped<IMapperLocationAdvertisingPlatform, MapperLocationAdvertisingPlatform>();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
    }

    public void Configure(IApplicationBuilder app)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCustomExceptionHandler();
        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}