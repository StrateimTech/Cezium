using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Api.Protocol;
using GenHTTP.Engine;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Placeholders;
using GenHTTP.Modules.Practices;
using GenHTTP.Modules.Websites;
using GenHTTP.Themes.AdminLTE;
using Main.Utils;

namespace Main.Web
{
    public class WebHandler
    {
        private readonly ushort _port;
        private readonly IServerHost _serverHost;

        private const string HomeHtmlPath = "./Web/Pages/Home/Home.html";
        private const string ProfilesHtmlPath = "./Web/Pages/Profiles/Profiles.html";
        private const string SettingsHtmlPath = "./Web/Pages/Settings/Settings.html";
        private const string SidebarHtmlPath = "./Web/Pages/Sidebar/Sidebar.html";
        
        public WebHandler(ushort port)
        {
            _port = port;
            _serverHost = Host.Create();
        }
        
        public void Start()
        {
            var project = Layout.Create()
                            .Index(Page.From("Home", File.ReadAllText(HomeHtmlPath)))
                            .Add("Settings", Page.From("Settings", File.ReadAllText(SettingsHtmlPath)))
                            .Add("Profiles", Page.From("Profiles", File.ReadAllText(ProfilesHtmlPath)));
            
            var theme = Theme
                            .Create()
                            .Title("Cezium Panel")
                            .FooterLeft((_, _) => "Project by Strateim (https://Strateim.tech)")
                            .FooterRight((_, _) => "Version 1.0.0")
                            .Sidebar((_, _) => File.ReadAllText(SidebarHtmlPath));

            var website = Website.Create()
                            .Theme(theme)
                            .Content(project);
            
            _serverHost
                            .Port(_port)
                            .Defaults()
                            .Console()
                            .Development()
                            .Handler(website)
                            .Run();
        }

        public void Restart()
        {
            _serverHost.Restart();
        }
        
        public void Stop()
        {
            _serverHost.Stop();
        }
    }
}