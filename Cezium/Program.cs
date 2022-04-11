using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Cezium.Rust;
using Cezium.Utils;
using Cezium.Web.API;
using Cezium.Web.Front;
using Figgle;
using HID_API;

namespace Cezium
{
    class Program
    {
        public static readonly TimeZoneInfo TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "America/Los_Angeles" : "Pacific Standard Time");
        
        static void Main(string[] args)
        {
            Console.Clear();
            if (File.Exists("Assets/ANSI Shadow.flf"))
            {
                using var fontStream = File.OpenRead("Assets/ANSI Shadow.flf");
                var font = FiggleFontParser.Parse(fontStream);

                Console.WriteLine();

                var figgleLines = Regex.Split(font.Render("Cezium"), "\r\n|\r|\n");
                for (int i = 0; i < figgleLines.Length - 1; i++)
                {
                    ConsoleUtils.WriteCentered(figgleLines[i]);
                }

                var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo);
                var formattedTime = dateTime.ToString("MM/dd/yy HH:mm:ss");
                ConsoleUtils.WriteCentered($"> {formattedTime} <\n");
            }
            else
            {
                Console.WriteLine();
                ConsoleUtils.WriteLine("Couldn't load FiggleFont font continuing... (Assets/ANSI Shadow.flf) \n");
            }

#if RELEASE
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ConsoleUtils.WriteLine($"Platform not supported! Please use raspbian or linux alternative! ({Environment.OSVersion})");
                return;
            }
#endif

            ConsoleUtils.WriteLine("Starting...");

            // TODO: Implement a better arguments system instead of hardcoding path values (for me)
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
            new Thread(() => rustHandler.Start())
            {
                IsBackground = true
            }.Start();
            
            // TODO: Same thing here > Implement a better arguments system
            if (args.Length >= 1)
            {
                foreach (var arg in args)
                {
                    if (arg.Equals("--api", StringComparison.OrdinalIgnoreCase))
                    {
                        var apiPorts = new ushort[] {300, 301};
                        var apiHandler = new ApiHandler(apiPorts, rustHandler, hidHandler);
                        ConsoleUtils.WriteLine($"Starting WebAPI on port(s) ({string.Join(", ", apiPorts)})");
                        new Thread(() => apiHandler.Start())
                        {
                            IsBackground = true
                        }.Start();
                        break;
                    }
                }
            }

            var frontPorts = new ushort[] {80, 443};
            var frontHandler = new FrontHandler(frontPorts, rustHandler);
            ConsoleUtils.WriteLine($"Starting WebServer on port(s) ({string.Join(", ", frontPorts)})");
            new Thread(() => frontHandler.Start())
            {
                IsBackground = true
            }.Start();

            ConsoleUtils.WriteLine("Successfully started!");

            // TODO: Implement setting saving to a file (json)
            Console.CancelKeyPress += (_, _) =>
            {
                ConsoleUtils.WriteLine("Shutting down...");
                hidHandler.Stop();
                rustHandler.Stop();
                frontHandler.Stop();
                Environment.Exit(0);
            };

            ConsoleUtils.WriteLine("Press Ctrl+C to continue & shutdown!");
            Console.ReadKey();
        }
    }
}