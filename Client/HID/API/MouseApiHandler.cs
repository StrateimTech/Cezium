using System;
using Client.API;

namespace Client.HID.API
{
    public class MouseApiHandler : IApiHandler
    {
        private readonly Settings _settings;
        
        public MouseApiHandler(Settings settings)
        {
            _settings = settings;
        }
        
        public string? HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[1])
                {
                    case "MouseState":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.Mouse.MouseState = value;
                    }
                        break;
                    case "InvertMouseY":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.Mouse.InvertMouseY = value;
                    }
                        break;
                    case "InvertMouseX":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.Mouse.InvertMouseX = value;
                    }
                        break;
                    case "InvertMouseWheel":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.Mouse.InvertMouseWheel = value;
                    }
                        break;
                    case "DebugState":
                    {
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.Mouse.DebugState = value;
                    } 
                        break;
                }
            }
            return null;
        }
    }
}