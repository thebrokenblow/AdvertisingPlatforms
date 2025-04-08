using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AdvertisingPlatforms.WebApi.Configurations;

public class ConfigureSwaggerOptions() : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc(
            "v1",
            new()
            {
                Title = "AdvertisingPlatforms API v1",
                Version = "v1",
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