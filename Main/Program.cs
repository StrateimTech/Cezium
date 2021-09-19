// See https://aka.ms/new-console-template for more information
using System;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
using Main;
using Main.Games.Apex;
using Main.Games.Rust;
using Main.Games.Vanguard;
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

ConsoleUtils.WriteCentered($"Please select and type a game ({string.Join(", ", Enum.GetNames(typeof(Settings.Game)))})");
var selectedGame = Console.ReadLine();
Enum.TryParse(selectedGame, out Settings.Game game);
settings.General.CurrentGame = game;

switch (game)
{
    case Settings.Game.Rust:
        RustHandler rustHandler = new(settings, hidHandler);
        var rustThreadHandler = new Thread(() =>
        {
            rustHandler.Start();
        })
        {
                        Name = "rustThreadHandler",
                        IsBackground = true
        };
        rustThreadHandler.Start();
        break;
    case Settings.Game.Apex:
        ApexHandler apexHandler = new(settings, hidHandler);
        var apexThreadHandler = new Thread(() =>
        {
            apexHandler.Start();
        })
        {
                        Name = "apexThreadHandler",
                        IsBackground = true
        };
        apexThreadHandler.Start();
        break;
    case Settings.Game.Vanguard:
        VanguardHandler vanguardHandler = new(settings, hidHandler);
        var vanguardThreadHandler = new Thread(() =>
        {
            vanguardHandler.Start();
        })
        {
                        Name = "vanguardThreadHandler",
                        IsBackground = true
        };
        vanguardThreadHandler.Start();
        break;
}

ConsoleUtils.WriteCentered("Press any key to continue...");
Console.ReadKey(true);
hidHandler.Stop();