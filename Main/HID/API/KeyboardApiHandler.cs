using System;
using Main.API;

namespace Main.HID.API
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
            throw new NotImplementedException();
        }
    }
}