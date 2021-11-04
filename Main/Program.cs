using System;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
using Main;
using Main.API;
using Main.HID;
using Main.Rust;
using Main.Utils;

var figgleText = FiggleFonts.Isometric3.Render("CEZIUM");
var figgleLines = Regex.Split(figgleText, "\r\n|\r|\n");
foreach (var figgleLine in figgleLines)
{
    ConsoleUtils.WriteCentered(figgleLine);
}
ConsoleUtils.WriteCentered("Project by StrateimTech (https://Strateim.tech)");

ConsoleUtils.WriteCentered("Initializing default settings.");
var settings = new Settings();

ConsoleUtils.WriteCentered($"Starting Human Interface Device handler.");
HidHandler hidHandler = new(settings);

if (hidHandler.HidMouseHandler != null)
{
    ConsoleUtils.WriteCentered($"Starting Rust handler.");
    RustHandler rustHandler = new(settings, hidHandler);
    var rustThreadHandler = new Thread(() =>
    {
        rustHandler.Start();
    })
    {
        IsBackground = true
    };
    rustThreadHandler.Start();
    
    ConsoleUtils.WriteCentered("Starting API server on port 200");
    var apiThreadHandler = new Thread(() => new ApiServer(200, rustHandler, hidHandler, settings))
    {
        IsBackground = true
    };
    apiThreadHandler.Start();
}
else
{
    ConsoleUtils.WriteCentered($"Cannot start Rust handler without a mouse connected.");
}

ConsoleUtils.WriteCentered("Press any key to continue...");
Console.ReadKey(true);
hidHandler.Stop();
