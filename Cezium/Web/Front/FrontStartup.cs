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
            RequestPath = "/Web/Front/Assets",
            OnPrepareResponse = context =>
            {
                context.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Context.Response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, POST");
                context.Context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Depth, User-Agent, X-File-Size, X-Requested-With, If-Modified-Since, X-File-Name, Cache-Control");
            }
        });
        
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, POST");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Depth, User-Agent, X-File-Size, X-Requested-With, If-Modified-Since, X-File-Name, Cache-Control");
            await next.Invoke();
        });

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
    }
}