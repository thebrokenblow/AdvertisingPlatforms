namespace AdvertisingPlatforms.WebApi;

public partial class Program
{
    public static void Main(string[] args)
    {
        var app = CreateHostBuilder(args)
           .Build();

        app.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
}