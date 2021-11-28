using System;
using System.Runtime.InteropServices;
using System.Threading;
using Client.API;
using Client.HID;
using Client.Rust;
using Client.Utils;

namespace Client
{
    public class Program
    {
        public void Main(string[] args)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ConsoleUtils.WriteLine($"Platform unsupported please use Raspbian or linux alternative. ({Environment.OSVersion})");
                return;
            }
            
            ConsoleUtils.WriteLine("Loading Cezium client");
            
            ConsoleUtils.WriteLine("Initializing default settings.");
            var settings = new Settings();

            ConsoleUtils.WriteLine("Starting Human Interface Device handler.");
            HidHandler hidHandler = new(settings);

            if (hidHandler.HidMouseHandler != null)
            {
                ConsoleUtils.WriteLine("Starting Rust handler.");
                RustHandler rustHandler = new(settings, hidHandler);
                var rustThreadHandler = new Thread(() =>
                {
                    rustHandler.Start();
                })
                {
                    IsBackground = true
                };
                rustThreadHandler.Start();
    
                ConsoleUtils.WriteLine("Starting API server on port 200");
                var apiThreadHandler = new Thread(() => new ApiServer(200, rustHandler, hidHandler, settings))
                {
                    IsBackground = true
                };
                apiThreadHandler.Start();
            }
            else
            {
                ConsoleUtils.WriteLine("Cannot start Rust handler without a mouse connected.");
            }

            ConsoleUtils.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            hidHandler.Stop();
        }
    }
}