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

        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSingleton<IAdvertisingRegion, AdvertisingRegion>();

        services.AddSingleton<IMapperLocationElements, MapperLocationElements>();
        services.AddSingleton<IMapperLocationAdvertisingPlatform, MapperLocationAdvertisingPlatform>();

        services.AddSingleton<ValidatorMapperLocationElements>();
        services.AddSingleton<ValidatorMapperLocationAdvertisingPlatform>();
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