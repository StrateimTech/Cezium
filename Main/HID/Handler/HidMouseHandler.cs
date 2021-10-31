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

        #region keycode

        public enum KEY_CODE
        {
            KEY_1 = 2,
            KEY_2,
            KEY_3,
            KEY_4,
            KEY_5,
            KEY_6,
            KEY_7,
            KEY_8,
            KEY_9,
            KEY_0,
            KEY_MINUS,
            KEY_EQUAL,
            KEY_BACKSPACE,
            KEY_TAB,
            KEY_Q,
            KEY_W,
            KEY_E,
            KEY_R,
            KEY_T,
            KEY_Y,
            KEY_U,
            KEY_I,
            KEY_O,
            KEY_P,
            KEY_LEFTBRACE,
            KEY_RIGHTBRACE,
            KEY_ENTER,
            KEY_LEFTCTRL,
            KEY_A,
            KEY_S,
            KEY_D,
            KEY_F,
            KEY_G,
            KEY_H,
            KEY_J,
            KEY_K,
            KEY_L,
            KEY_SEMICOLON,
            KEY_APOSTROPHE,
            KEY_GRAVE,
            KEY_LEFTSHIFT,
            KEY_BACKSLASH,
            KEY_Z,
            KEY_X,
            KEY_C,
            KEY_V,
            KEY_B,
            KEY_N,
            KEY_M,
            KEY_COMMA,
            KEY_DOT,
            KEY_SLASH,
            KEY_RIGHTSHIFT,
            KEY_KPASTERISK,
            KEY_LEFTALT,
            KEY_SPACE,
            KEY_CAPSLOCK,
            KEY_F1,
            KEY_F2,
            KEY_F3,
            KEY_F4,
            KEY_F5,
            KEY_F6,
            KEY_F7,
            KEY_F8,
            KEY_F9,
            KEY_F10,
            KEY_NUMLOCK,
            KEY_SCROLLLOCK,
            KEY_KP7,
            KEY_KP8,
            KEY_KP9,
            KEY_KPMINUS,
            KEY_KP4,
            KEY_KP5,
            KEY_KP6,
            KEY_KPPLUS,
            KEY_KP1,
            KEY_KP2,
            KEY_KP3,
            KEY_KP0,
            KEY_KPDOT
        }

        #endregion
        
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