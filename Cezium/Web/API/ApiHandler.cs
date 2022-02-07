using Cezium.Rust;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cezium.Web.API;

public class ApiHandler
{
    public static RustHandler RustHandler;
    private readonly IHost _builder;
    
    public ApiHandler(RustHandler rustHandler)
    {
        RustHandler = rustHandler;
        _builder = CreateHostBuilder().Build();
    }

    public void Start()
    {
        _builder.Run();
    }

    public void Stop()
    {
        _builder.StopAsync();
    }

    private static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureLogging(_ => _.ClearProviders());
                webBuilder.UseUrls("http://*:200;https://*:201");
                webBuilder.UseStartup<ApiStartup>();
            });
}