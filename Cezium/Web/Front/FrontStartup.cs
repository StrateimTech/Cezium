using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Cezium.Web.Front;

public class FrontStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RazorPagesOptions>(options => { options.RootDirectory = "/Web/Front/Pages"; });
        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();

        app.UseStaticFiles();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "Assets")),
            RequestPath = "/Web/Front/Assets"
        });

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
    }
}