// See https://aka.ms/new-console-template for more information

using System;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
using Main;
using Main.API;
using Main.HID;
using Main.Utils;
using Main.Web;

const int webPort = 4996;
const int apiPort = 4995;

var figgleText = FiggleFonts.Isometric3.Render("CEZIUM");
var figgleLines = Regex.Split(figgleText, "\r\n|\r|\n");
foreach (var figgleLine in figgleLines)
{
    ConsoleUtils.WriteCentered(figgleLine);
}
ConsoleUtils.WriteCentered("Project by StrateimTech (https://Strateim.tech)");

ConsoleUtils.WriteCentered("Initializing default settings.");
var settings = new Settings();

// ConsoleUtils.WriteCentered($"Starting API handler. (Port: {apiPort}, https://localhost:{apiPort})");
// ApiHandler? apiHandler = null;
// var apiThreadHandler = new Thread(() =>
// {
//     apiHandler = new ApiHandler(apiPort, settings);
//     apiHandler.Start();
// })
// { 
//                 IsBackground = true
// };
// apiThreadHandler.Start();


ConsoleUtils.WriteCentered($"Starting web interface handler. (Port: {webPort}, https://localhost:{webPort})");
WebHandler? webHandler = null;
var webThreadHandler = new Thread(() =>
{
    webHandler = new WebHandler(webPort);
    webHandler.Start();
})
{
                IsBackground = true
};
webThreadHandler.Start();

// ConsoleUtils.WriteCentered($"Starting spoofed Human Interface Device handler.");
//
// HidHandler? hidHandler = null;
// var hidThreadHandler = new Thread(() =>
// {
//     hidHandler = new HidHandler(settings);
//     hidHandler.Start();
// })
// {
//                 IsBackground = true
// };
// hidThreadHandler.Start();

ConsoleUtils.WriteCentered("Press any key to continue...");
Console.ReadKey(true);
webHandler?.Stop();
// apiHandler?.Stop();
// hidHandler?.Stop();