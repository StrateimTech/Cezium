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
    private static ushort[] _ports;

    public ApiHandler(ushort[] ports, RustHandler rustHandler, HidHandler hidHandler)
    {
        RustHandler = rustHandler;
        HidHandler = hidHandler;
        _ports = ports;
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
                webBuilder.UseUrls($"http://*:{_ports[0]};https://*:{_ports[1]}");
                webBuilder.UseStartup<ApiStartup>();
            });
}