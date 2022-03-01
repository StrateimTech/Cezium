using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cezium.Web.Front;

public class FrontHandler
{
    private readonly IHost _builder;
    public static readonly string Server = "http://127.0.0.1:300/";

    public FrontHandler()
    {
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
                webBuilder.UseUrls("http://*:80;https://*:443");
                webBuilder.UseStartup<FrontStartup>();
            });
}