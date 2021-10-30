
using System;
using Main.HID;
using Main.Rust;

namespace Main.API
{
    public class ApiHandler : IApiHandler
    {
        private readonly RustHandler _rustHandler;
        private readonly HidHandler _hidHandler;
        
        public ApiHandler(RustHandler rustHandler, HidHandler hidHandler)
        {
            _rustHandler = rustHandler;
            _hidHandler = hidHandler;
        }
        
        public void HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[0])
                {
                    // Mouse
                    case "0":
                        _hidHandler.MouseApiHandler.HandlePacket(data);
                        break;
                    // Keyboard
                    case "1":
                        _hidHandler.KeyboardApiHandler.HandlePacket(data);
                        break;
                    // Rust
                    case "2":
                        _rustHandler.RustApiHandler.HandlePacket(data);
                        break;
                }
            }
        }
    }
}