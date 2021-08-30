// See https://aka.ms/new-console-template for more information

using System;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
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

ConsoleUtils.WriteCentered($"Starting API. (Port: {apiPort}, https://localhost:{apiPort})");
ApiHandler? apiHandler = null;
var apiThreadHandler = new Thread(() =>
{
    apiHandler = new ApiHandler(apiPort);
    apiHandler.Start();
})
{ 
                IsBackground = true
};
apiThreadHandler.Start();


ConsoleUtils.WriteCentered($"Starting web interface. (Port: {webPort}, https://localhost:{webPort})");
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

ConsoleUtils.WriteCentered("Press any key to continue...");
Console.ReadKey(true);
webHandler?.Stop();
apiHandler?.Stop();

// var hidHandler = new HidHandler();