// See https://aka.ms/new-console-template for more information

using System;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
using Main;
using Main.HID;
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
var hidThreadHandler = new Thread(() =>
{
    hidHandler.Start();
})
{
                Name = "HidHandlerThread",
                IsBackground = true
};
hidThreadHandler.Start();


ConsoleUtils.WriteCentered("Press any key to continue...");
Console.ReadKey(true);
hidHandler.Stop();