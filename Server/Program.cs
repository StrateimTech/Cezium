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
        private const string AssemblyFolder = @"E:\Programming\Projects\StrateimTech\Cezium\Client\publish\";

        public static byte[] ClientAssembly;

        public static void Main(string[] args)
        {
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
            new Thread(() => assemblyLoader.StartWatcher(AssemblyFolder)).Start();
            var assembly = AssemblyLoader.FindLatestAssembly(AssemblyFolder);

            if (assembly == null)
            {
                ConsoleUtils.WriteLine("Cezium client assembly couldn't be found or loaded!", "Server");
                ConsoleUtils.WriteLine("Shutting the server down!", "Server");
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
        }
    }
}