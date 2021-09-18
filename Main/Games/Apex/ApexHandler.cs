using System;
using System.Collections;
using System.Threading;
using Main.HID;
using Main.Utils;

namespace Main.Games.Apex
{
    public class ApexHandler
    {
        private readonly Settings _settings;
        
        private readonly HidHandler _hidHandler;
        private readonly Random _random = new Random();
        
        public ApexHandler(Settings settings, HidHandler hidHandler)
        {
            _settings = settings;
            _hidHandler = hidHandler;
        }

        public void Start()
        {
            while (true)
            {
                if (_hidHandler.LeftButton && _hidHandler.RightButton)
                {
                    FileUtils.write_mouse_report(_hidHandler.HidFileStream, BitUtils.ToByte(_hidHandler.ButtonBitArray), new[] {Convert.ToSByte(_random.Next(-128, 127)), Convert.ToSByte(_random.Next(-128, 127))});
                }
            }
        }
    }
}