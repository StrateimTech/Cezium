﻿using System.Diagnostics;

namespace Client.API
{
    public class GeneralApiHandler : IApiHandler
    {
        private readonly Settings _settings;
        private readonly Stopwatch _stopwatch;
        
        public GeneralApiHandler(Settings settings)
        {
            _settings = settings;
            _stopwatch = Stopwatch.StartNew();
        }
        
        public string? HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[1])
                {
                    case "GetUptime":
                        return $"{_stopwatch.Elapsed:hh\\:mm\\:ss}";
                    case "GetMouseState":
                        return $"{_settings.General.Mouse.MouseState}";
                    case "GetKeyboardState":
                        return $"{_settings.General.Keyboard.KeyboardState}";
                    case "GetRustState":
                        return $"{_settings.Rust.State}";
                    case "GetServerVersion":
                        return $"{_settings.Version}";
                }
            }

            return null;
        }
    }
}