using Microsoft.AspNetCore.Hosting;

namespace AdvertisingPlatforms.IntegrationTests.Utilities;

public class CustomApiFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
    }
}