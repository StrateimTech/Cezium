using System;
using System.IO;
using System.Threading;
using Main.Utils;

namespace Main.HID.Handler
{
    public class HidKeyboardHandler
    {
        public HidKeyboardHandler(HidHandler hidHandler, Settings settings, FileStream keyboardFileStream, FileStream hidFileStream) 
        {
            new Thread(() =>
            {
                while (true)
                {
                    byte[] buffer = new byte[24];
                    keyboardFileStream.Read(buffer, 0, buffer.Length);

                    int offset = 8;
                    // offset 8 bytes for timeval
                    short type = BitConverter.ToInt16(new byte[] { buffer[offset], buffer[++offset] }, 0);
                    short code = BitConverter.ToInt16(new byte[] { buffer[++offset], buffer[++offset] }, 0);
                    int value = BitConverter.ToInt32(new byte[] { buffer[++offset], buffer[++offset], buffer[++offset], buffer[++offset] }, 0);
                
                    ConsoleUtils.WriteCentered($"Code={code}, Value={value}, Type={type}");
                    ConsoleUtils.WriteCentered($"Key: {(((HidMouseHandler.KEY_CODE)code).ToString()).Replace("KEY_", "")}");
                }
            }).Start();
        }
    }
}