using System;
using Main.API;

namespace Main.HID.API
{
    public class MouseApiHandler : IApiHandler
    {
        private readonly Settings _settings;
        
        public MouseApiHandler(Settings settings)
        {
            _settings = settings;
        }
        
        public void HandlePacket(string[] data)
        {
            if (data.Length > 0)
            {
                switch (data[1])
                {
                    case "MouseState":
                        Boolean.TryParse(data[2], out bool value);
                        _settings.General.MouseState = value;
                        break;
                    case "InvertMouseY":
                        Boolean.TryParse(data[2], out bool value2);
                        _settings.General.InvertMouseY = value2;
                        break;
                    case "InvertMouseX":
                        Boolean.TryParse(data[2], out bool value3);
                        _settings.General.InvertMouseX = value3;
                        break;
                    case "InvertMouseWheel":
                        Boolean.TryParse(data[2], out bool value4);
                        _settings.General.InvertMouseWheel = value4;
                        break;
                }
            }
        }
    }
}