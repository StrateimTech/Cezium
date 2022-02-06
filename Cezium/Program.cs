using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Cezium.Rust;
using Client.Utils;
using Figgle;
using HID_API;

namespace Cezium
{
    class Program
    {
        private static readonly TimeZoneInfo TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "America/Los_Angeles" : "Pacific Standard Time");
        
        static void Main(string[] args)
        {
            Console.Clear();
            if (File.Exists("Assets/ANSI Shadow.flf"))
            {
                using var fontStream = File.OpenRead("Assets/ANSI Shadow.flf");
                var font = FiggleFontParser.Parse(fontStream);

                Console.WriteLine();

                var figgleLines = Regex.Split(font.Render("Cezium"), "\r\n|\r|\n");
                for (int i = 0; i < figgleLines.Length-1; i++)
                {
                    ConsoleUtils.WriteCentered(figgleLines[i]);
                }
                
                var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo);
                var formattedTime = dateTime.ToString("MM/dd/yyyy HH:mm:ss");
                ConsoleUtils.WriteCentered($"> {formattedTime} <\n");
            }
            else
            {
                Console.WriteLine();
                ConsoleUtils.WriteLine("Couldn't load FiggleFont font continuing... (Assets/ANSI Shadow.flf) \n");
            }   
            
            ConsoleUtils.WriteLine("Starting...");
            
            var hidHandler = new HidHandler(
                new[]
                {
                    "/dev/input/mice"
                }, 
                new[]
                {
                    "/dev/input/by-id/usb-Logitech_G413_Silver_Mechanical_Gaming_Keyboard_1172384C3230-event-kbd",
                    "/dev/input/by-id/usb-Logitech_G502_HERO_Gaming_Mouse_0E6D395D3333-if01-event-kbd"
                }, 
                "/dev/hidg0");
            
            var rustHandler = new RustHandler(hidHandler);
            var rustThreadHandler = new Thread(() =>
            {
                rustHandler.Start();
            });
            rustThreadHandler.Start();
            
            ConsoleUtils.WriteLine("Successfully started!");

            Console.CancelKeyPress += (_, _) => 
            {
                ConsoleUtils.WriteLine("Shutting down...");
                hidHandler.Stop();
                rustHandler.Stop();
            };
            
            Console.ReadKey();
        }
    }
}