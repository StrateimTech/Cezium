﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cezium.Web.Front;

public class FrontHandler
{
    private readonly IHost _builder;
    
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
                webBuilder.UseContentRoot($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Web{Path.DirectorySeparatorChar}Front");
                webBuilder.ConfigureLogging(_ => _.ClearProviders());
                webBuilder.UseUrls("http://*:200;https://*:201");
                webBuilder.UseStartup<FrontStartup>();
            });
}