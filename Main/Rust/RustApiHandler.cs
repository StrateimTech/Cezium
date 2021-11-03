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
        
        public string? HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[1])
                {
                    case "ChangeState":
                        Boolean.TryParse(data[2], out bool value);
                        _settings.Rust.State = value;
                        break;
                    case "ChangeFov":
                        Int32.TryParse(data[2], out Int32 value2);
                        _settings.Rust.Fov = value2;
                        _rustHandler.PixelTable = _rustHandler.CalculatePixelTables(_rustHandler.CurrentWeapon.Item2);
                        break;
                    case "ChangeSens":
                        Double.TryParse(data[2], out double value3);
                        _settings.Rust.Sensitivity = value3;
                        _rustHandler.PixelTable = _rustHandler.CalculatePixelTables(_rustHandler.CurrentWeapon.Item2);
                        break;
                    case "ChangeGun":
                        _rustHandler.UpdateGun(data[2], data[3], data[4]);
                        break;
                    case "ChangeSmoothness":
                        Int32.TryParse(data[2], out Int32 value4);
                        _settings.Rust.Smoothness = value4;
                        break;
                    case "ChangeAmmoReset":
                        Boolean.TryParse(data[2], out bool value5);
                        _settings.Rust.AmmoReset = value5;
                        break;
                    
                    case "ChangeRandomization":
                        Boolean.TryParse(data[2], out bool value6);
                        _settings.Rust.Randomization = value6;
                        break;
                    case "ChangeReverseRandomization":
                        Boolean.TryParse(data[2], out bool value7);
                        _settings.Rust.ReverseRandomization = value7;
                        break;
                    case "ChangeRandomizationAmountX":
                        Int32.TryParse(data[2], out Int32 value8);
                        Int32.TryParse(data[3], out Int32 value9);
                        _settings.Rust.RandomizationAmountX = new(value8, value9);
                        break;
                    case "ChangeRandomizationAmountY":
                        Int32.TryParse(data[2], out Int32 value10);
                        Int32.TryParse(data[3], out Int32 value11);
                        _settings.Rust.RandomizationAmountY = new(value10, value11);
                        break;
                    case "ChangeRecoilModifierX":
                        Double.TryParse(data[2], out double value12);
                        _settings.Rust.RecoilModifier = new(value12, _settings.Rust.RecoilModifier.Item2);
                        break;
                    case "ChangeRecoilModifierY":
                        Double.TryParse(data[2], out double value13);
                        _settings.Rust.RecoilModifier = new(_settings.Rust.RecoilModifier.Item1, value13);
                        break;
                }
            }
            return null;
        }
    }
}