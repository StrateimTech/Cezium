using System;
using System.Collections;
using System.IO;
using Main.Utils;

namespace Main.HID
{
    public class HidHandler
    {
        private readonly Settings _settings;
        private bool _running;
        
        // private string KeyboardStreamPath = "";
        private string MouseStreamPath = "/dev/input/mice";
        
        private string HumanInterfaceDeviceStreamPath = "/dev/hidg0";

        // public readonly FileStream KeyboardFileStream;
        public readonly FileStream MouseFileStream;
        
        public readonly FileStream HidFileStream;

        public bool LeftButton;
        public bool RightButton;
        public bool MiddleButton;

        public BitArray ButtonBitArray;
        

        public HidHandler(Settings settings)
        {
            _settings = settings;
            // _keyboardFileStream = File.Open(KeyboardStreamPath, FileMode.Open, FileAccess.Read);
            MouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.Read); 
            
            HidFileStream = File.Open(HumanInterfaceDeviceStreamPath, FileMode.Open, FileAccess.Write);
        }

        public void Start()
        {
            _running = true;
            while (_running)
            {
                var mouseSbyteArray = ReadSByte(MouseFileStream);
                // var keyboardSbyteArray = ReadSByte(KeyboardFileStream);
                if (mouseSbyteArray.Length > 0)
                {
                    mouseSbyteArray[1] = _settings.General.InvertMouseX ? Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[1]) * -1) : mouseSbyteArray[1];
                    mouseSbyteArray[2] = _settings.General.InvertMouseY ? mouseSbyteArray[2] : Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[2]) * -1);
                    
                    LeftButton = (mouseSbyteArray[0] & 0x1) > 0;
                    RightButton = (mouseSbyteArray[0] & 0x2) > 0;
                    MiddleButton = (mouseSbyteArray[0] & 0x4) > 0;
                    var deltaX = Convert.ToInt32(mouseSbyteArray[1]);
                    var deltaY = Convert.ToInt32(mouseSbyteArray[2]);
                    var deltaWheel = mouseSbyteArray[3];
                    
                    ButtonBitArray = new BitArray(new[]
                    {
                                    LeftButton, RightButton, MiddleButton, false, false, false, false, false
                    });
                    FileUtils.write_mouse_report(HidFileStream, BitUtils.ToByte(ButtonBitArray), new[] {Convert.ToSByte(deltaX), Convert.ToSByte(deltaY)});
                }
            }
        }

        public void Stop()
        {
            _running = false;
            // KeyboardFileStream.Close();
            MouseFileStream.Close();
            HidFileStream.Close();
        }

        private sbyte[] ReadSByte(FileStream fileStream, int length = 4)
        {
            var byteArray = new byte[length];
            fileStream.Read(byteArray, 0, byteArray.Length);
            var sbyteArray = new sbyte[byteArray.Length];
            Buffer.BlockCopy(byteArray, 0, sbyteArray, 0, byteArray.Length);
            return sbyteArray;
        }
    }
}