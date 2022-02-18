using Cezium.Rust;
using HID_API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cezium.Web.API;

public class ApiHandler
{
    public static RustHandler RustHandler;
    public static HidHandler HidHandler;
    private readonly IHost _builder;

    public ApiHandler(RustHandler rustHandler, HidHandler hidHandler)
    {
        RustHandler = rustHandler;
        HidHandler = hidHandler;
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
                webBuilder.UseUrls("http://*:300;https://*:301");
                webBuilder.UseStartup<ApiStartup>();
            });
}