using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
using Server.Network;
using Server.Utils;

namespace Server
{
    class Program
    {
        public static readonly byte[] LatestClientAssembly = File.ReadAllBytes(@"E:\Programming\Projects\StrateimTech\Cezium\Client\publish\Client.dll");
        
        static void Main(string[] args)
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
                ConsoleUtils.WriteLine("Couldn't load FiggleFont font continuing. (Assets/ANSI Shadow.flf) \n", "Server");
            }

            if (LatestClientAssembly == null)
            {
                ConsoleUtils.WriteLine("Cezium client assembly couldn't be found or loaded!", "Server");
                ConsoleUtils.WriteLine("Shutting the server down!", "Server");
                return;
            }
            
            ConsoleUtils.WriteLine("Starting NetworkHandler", "Server");
            
            var networkHandler = new NetworkHandler();
            
            new Thread(() =>
            {
                networkHandler.Start(3000);
            }).Start();
            
            ConsoleUtils.WriteLine("Press any key to to continue & shutdown", "Server");
            Console.ReadKey(true);
            networkHandler.Stop();
            ConsoleUtils.WriteLine("Shutting the server down!", "Server");
        }
    }
}