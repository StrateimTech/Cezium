using System;
using Main.API;

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
        
        public string? HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[1])
                {
                    case "ChangeState":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.Rust.State = value;
                    }
                        break;
                    case "ChangeFov":
                    {
                        Int32.TryParse(data[2], out Int32 value);
                        _settings.Rust.Fov = value;
                        _rustHandler.PixelTable = _rustHandler.CalculatePixelTables(_rustHandler.CurrentWeapon.Item2);
                    }
                        break;
                    case "ChangeSens":
                    {
                        Double.TryParse(data[2], out double value);
                        _settings.Rust.Sensitivity = value;
                        _rustHandler.PixelTable = _rustHandler.CalculatePixelTables(_rustHandler.CurrentWeapon.Item2);
                    }
                        break;
                    case "ChangeGun":
                    {
                        _rustHandler.UpdateGun(data[2], data[3], data[4]);
                    }
                        break;
                    case "ChangeSmoothness":
                    {
                        Int32.TryParse(data[2], out Int32 value);
                        _settings.Rust.Smoothness = value;
                    }
                        break;
                    case "ChangeAmmoReset":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.Rust.AmmoReset = value;
                    }
                        break;

                    case "ChangeRandomization":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.Rust.Randomization = value;
                    }
                        break;
                    case "ChangeReverseRandomization":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.Rust.ReverseRandomization = value;
                    }

                        break;
                    case "ChangeRandomizationAmountX":
                    {
                        Int32.TryParse(data[2], out Int32 value);
                        Int32.TryParse(data[3], out Int32 value2);
                        _settings.Rust.RandomizationAmountX = new(value, value2);
                    }
                        break;
                    case "ChangeRandomizationAmountY":
                    {
                        Int32.TryParse(data[2], out Int32 value);
                        Int32.TryParse(data[3], out Int32 value2);
                        _settings.Rust.RandomizationAmountY = new(value, value2);
                    }
                        break;
                    case "ChangeRecoilModifierX":
                    {
                        Double.TryParse(data[2], out double value);
                        _settings.Rust.RecoilModifier = new(value, _settings.Rust.RecoilModifier.Item2);
                    }
                        break;
                    case "ChangeRecoilModifierY":
                    {
                        Double.TryParse(data[2], out double value);
                        _settings.Rust.RecoilModifier = new(_settings.Rust.RecoilModifier.Item1, value);
                    }
                        break;
                    case "DebugState":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.Rust.DebugState = value;
                    }
                        break;
                }
            }
            return null;
        }
    }
}