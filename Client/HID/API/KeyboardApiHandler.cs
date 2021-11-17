using System;
using Client.API;

namespace Client.HID.API
{
    public class KeyboardApiHandler : IApiHandler
    {
        private readonly Settings _settings;
        
        public KeyboardApiHandler(Settings settings)
        {
            _settings = settings;
        }
        
        public string? HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[1])
                {
                    case "KeyboardState":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.Keyboard.KeyboardState = value;
                    }
                        break;
                    case "DebugState":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.Keyboard.DebugState = value;
                    } 
                        break;
                }
            }
            return null;
        }
    }
}