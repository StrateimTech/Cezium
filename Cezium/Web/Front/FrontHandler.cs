﻿using System.IO;
using Cezium.Rust;
using HID_API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cezium.Web.Front;

public class FrontHandler
{
    private readonly IHost _builder;

    public static RustHandler RustHandler;
    private static ushort[] _ports;

    public FrontHandler(ushort[] ports, RustHandler rustHandler)
    {
        RustHandler = rustHandler;
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
                webBuilder.UseUrls($"http://*:{_ports[0]};");
                webBuilder.UseStartup<FrontStartup>();
            });
}