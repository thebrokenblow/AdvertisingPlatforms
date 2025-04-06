using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AdvertisingPlatforms.WebApi.Configurations;

public class ConfigureSwaggerOptions() : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("AdvertisingPlatforms",
               new OpenApiInfo
               {
                   Version = "1.0",
                   Title = $"AdvertisingPlatforms API {1.0}",
                   Description = "Простой веб сервис, позволяющий хранить и возвращать списки рекламных площадок для заданной локации в запросе.",
                   Contact = new OpenApiContact
                   {
                       Name = " Artem Krasov",
                       Email = "artkrasov@gmail.com",
                       Url = new Uri("https://t.me/krasovart")
                   },
               });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}