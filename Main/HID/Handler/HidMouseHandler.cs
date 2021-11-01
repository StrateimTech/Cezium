using System;
using System.Collections;
using System.IO;
using System.Threading;
using Main.Utils;

namespace Main.HID.Handler
{
    public class HidMouseHandler
    {
        public Mouse Mouse { get; private set; } = new();
        
        public HidMouseHandler(HidHandler hidHandler, Settings settings, FileStream mouseFileStream, FileStream hidFileStream)
        {
            new Thread(() =>
            {
                FileUtils.WriteReport(mouseFileStream, new byte[] {0xf3, 200, 0xf3, 100, 0xf3, 80});
                while (true)
                {
                    if (settings.General.Mouse.MouseState)
                    {
                        var mouseSbyteArray = BitUtils.ReadSByteFromStream(mouseFileStream);
                        if (mouseSbyteArray.Length > 0)
                        {
                            mouseSbyteArray[1] = settings.General.Mouse.InvertMouseX ? Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[1]) * -1) : mouseSbyteArray[1];
                            mouseSbyteArray[2] = settings.General.Mouse.InvertMouseY ? mouseSbyteArray[2] : Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[2]) * -1);
                            mouseSbyteArray[3] = settings.General.Mouse.InvertMouseWheel ? mouseSbyteArray[3] : Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[3]) * -1);
                        
                            hidHandler.WriteMouseReport(Mouse = new Mouse()
                            {
                                LeftButton = (mouseSbyteArray[0] & 0x1) > 0,
                                RightButton = (mouseSbyteArray[0] & 0x2) > 0,
                                MiddleButton = (mouseSbyteArray[0] & 0x4) > 0,
                                X = Convert.ToInt32(mouseSbyteArray[1]),
                                Y = Convert.ToInt32(mouseSbyteArray[2]),
                                Wheel = Convert.ToInt32(mouseSbyteArray[3]),
                                ButtonBitArray = new BitArray(new[]
                                {
                                    (mouseSbyteArray[0] & 0x1) > 0, (mouseSbyteArray[0] & 0x2) > 0, (mouseSbyteArray[0] & 0x4) > 0, false, false, false, false, false
                                })
                            });
                        }
                    }
                }
            }).Start();
        }
    }
}