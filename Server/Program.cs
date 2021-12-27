using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
using Server.Assembly;
using Server.Network;
using Server.Utils;

namespace Server
{
    class Program
    {
        private static string _assemblyFolder;

        public static byte[] ClientAssembly;

        public static readonly Settings Settings = new ();
        
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var argument in args)
                {
                    var command = argument.Replace("--", "");
                    switch (command.ToLower())
                    {
                        case "noobfuscation":
                            Settings.Obfuscation = false;
                            break;
                        case "hideip":
                            Settings.HideIP = true;
                            break;
                        default:
                            ConsoleUtils.WriteLine($"Unknown command ({command})", "Server");
                            break;
                    }
                }
            }
            
            _assemblyFolder = Path.Combine(Directory.GetCurrentDirectory(), "Clients");
            if (File.Exists("Assets/ANSI Shadow.flf"))
            {
                using var fontStream = File.OpenRead("Assets/ANSI Shadow.flf");
                var font = FiggleFontParser.Parse(fontStream);

                Console.WriteLine();

                var figgleLines = Regex.Split(font.Render("Cezium Server"), "\r\n|\r|\n");
                foreach (var figgleLine in figgleLines)
                {
                    ConsoleUtils.WriteCentered(figgleLine);
                }
            }
            else
            {
                ConsoleUtils.WriteLine("Couldn't load FiggleFont font continuing. (Assets/ANSI Shadow.flf) \n",
                    "Server");
            }
            ConsoleUtils.WriteLine("Starting server", "Server");
            
            ConsoleUtils.WriteLine("Starting AssemblyLoader", "Server");
            AssemblyLoader assemblyLoader = new AssemblyLoader();
            new Thread(() => assemblyLoader.StartWatcher(_assemblyFolder)).Start();
            var assembly = AssemblyLoader.FindLatestAssembly(_assemblyFolder);

            if (assembly == null)
            {
                ConsoleUtils.WriteLine("Cezium client assembly couldn't be found or loaded!", "Server");
                ConsoleUtils.WriteLine("Shutting the server down!", "Server");
                // TODO: Hangs here
                Environment.Exit(0);
                return;
            }
            ConsoleUtils.WriteLine("Found & loaded latest assembly", "Server");
            ClientAssembly = assembly;
            
            ConsoleUtils.WriteLine("Starting NetworkHandler", "Server");
            
            var networkHandler = new NetworkHandler();
            
            new Thread(() => { networkHandler.Start(3000); }).Start();
            
            ConsoleUtils.WriteLine("Press any key to to continue & shutdown", "Server");
            Console.ReadKey(true);
            networkHandler.Stop();
            assemblyLoader.ResetEvent.Set();
            ConsoleUtils.WriteLine("Shutting the server down!", "Server");
            Environment.Exit(0);
        }
    }
}