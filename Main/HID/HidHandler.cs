using System;
using System.IO;

namespace Main.HID
{
    public class HidHandler
    {
        private string KeyboardStreamPath = "";
        private string MouseStreamPath = "/dev/input/mice";
        private string HumanInterfaceDeviceStreamPath = "/dev/hidg0";

        public FileStream KeyboardFileStream;
        public FileStream MouseFileStream;
        public FileStream HidFileStream;

        public HidHandler()
        {
            KeyboardFileStream = File.Open(KeyboardStreamPath, FileMode.Open, FileAccess.Read);
            MouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.Read); 
            HidFileStream = File.Open(HumanInterfaceDeviceStreamPath, FileMode.Open, FileAccess.Write);
        }

        public sbyte[] ReadSByte(FileStream fileStream, int length = 4)
        {
            var byteArray = new byte[length];
            fileStream.Read(byteArray, 0, byteArray.Length);
            var sbyteArray = new sbyte[byteArray.Length];
            Buffer.BlockCopy(byteArray, 0, sbyteArray, 0, byteArray.Length);
            return sbyteArray;
        }
    }
}