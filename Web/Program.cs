using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                        .ConfigureLogging((logging) =>
                        {
                            logging.ClearProviders();
                        })
                        .ConfigureWebHostDefaults(webBuilder => 
                        { 
                            webBuilder
                                            .UseStartup<Startup>()
                                            .UseUrls($"https://*:{(args.Length > 0 ? args[0] : 5001)}"); 
                        });
    }
}
