// See https://aka.ms/new-console-template for more information

using System;
using System.Text.RegularExpressions;
using Figgle;
using Main.HID;
using Main.Utils;

var figgleText = FiggleFonts.Isometric3.Render("CEZIUM");
var figgleLines = Regex.Split(figgleText, "\r\n|\r|\n");
foreach (var figgleLine in figgleLines)
{
    ConsoleUtils.WriteCentered(figgleLine);
}
ConsoleUtils.WriteCentered("Project by StrateimTech (https://Strateim.tech)");

// var hidHandler = new HidHandler();