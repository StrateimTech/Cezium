using System.IO;
using Cezium.Rust;
using HID_API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cezium.Web.Front;

public class FrontHandler
{
    private readonly IHost _builder;
    public static readonly string Server = "http://127.0.0.1:300/";

    public static RustHandler RustHandler;
    public static HidHandler HidHandler;
    private static ushort[] _ports;

    public FrontHandler(ushort[] ports, RustHandler rustHandler, HidHandler hidHandler)
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
                webBuilder.UseContentRoot(
                    $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Web{Path.DirectorySeparatorChar}Front");
                webBuilder.ConfigureLogging(_ => _.ClearProviders());
                webBuilder.UseUrls($"http://*:{_ports[0]};https://*:{_ports[1]}");
                webBuilder.UseStartup<FrontStartup>();
            });
}