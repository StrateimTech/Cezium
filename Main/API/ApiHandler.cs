
using System;
using Main.HID;
using Main.Rust;

namespace Main.API
{
    public class ApiHandler : IApiHandler
    {
        private readonly RustHandler _rustHandler;
        private readonly HidHandler _hidHandler;
        private readonly Settings _settings;
        private readonly GeneralApiHandler _generalApiHandler;

        public ApiHandler(RustHandler rustHandler, HidHandler hidHandler, Settings settings)
        {
            _rustHandler = rustHandler;
            _hidHandler = hidHandler;
            _settings = settings;
            _generalApiHandler = new(settings);
        }
        
        public string? HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[0])
                {
                    // Mouse
                    case "0":
                        return _hidHandler.MouseApiHandler.HandlePacket(data);
                    // Keyboard
                    case "1":
                        return _hidHandler.KeyboardApiHandler.HandlePacket(data);
                    // Rust
                    case "2":
                        return _rustHandler.RustApiHandler.HandlePacket(data);
                    // General
                    case "3":
                        return _generalApiHandler.HandlePacket(data);
                }
            }
            return null;
        }
    }
}