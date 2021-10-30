using System;
using Main.API;
using Main.Utils;

namespace Main.Rust
{
    public class RustApiHandler : IApiHandler
    {
        private readonly Settings _settings;
        private readonly RustHandler _rustHandler;
        
        public RustApiHandler(RustHandler rustHandler, Settings settings)
        {
            _settings = settings;
            _rustHandler = rustHandler;
        }
        
        public void HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[1])
                {
                    case "ChangeState":
                        Boolean.TryParse(data[2], out bool value);
                        ConsoleUtils.WriteCentered($"Updated state ({_settings.Rust.State} -> {value})", GetType().Name);
                        _settings.Rust.State = value;
                        break;
                    case "ChangeFov":
                        Int32.TryParse(data[2], out Int32 value2);
                        ConsoleUtils.WriteCentered($"Updated fov ({_settings.Rust.Fov} -> {value2})", GetType().Name);
                        _settings.Rust.Fov = value2;
                        _rustHandler.PixelTable = _rustHandler.CalculatePixelTables(_rustHandler.CurrentWeapon.Item2);
                        break;
                    case "ChangeSens":
                        Double.TryParse(data[2], out double value3);
                        ConsoleUtils.WriteCentered($"Updated sensitivity ({_settings.Rust.Sensitivity} -> {value3})", GetType().Name);
                        _settings.Rust.Sensitivity = value3;
                        _rustHandler.PixelTable = _rustHandler.CalculatePixelTables(_rustHandler.CurrentWeapon.Item2);
                        break;
                    case "ChangeGun":
                        ConsoleUtils.WriteCentered($"Updated gun ({data[2]})", GetType().Name);
                        _rustHandler.UpdateGun(data[2], data[3], data[4]);
                        break;
                    case "ChangeSmoothness":
                        Int32.TryParse(data[2], out Int32 value4);
                        ConsoleUtils.WriteCentered($"Updated smoothness ({_settings.Rust.Smoothness} -> {value4})", GetType().Name);
                        _settings.Rust.Smoothness = value4;
                        break;
                }
            }
        }
    }
}