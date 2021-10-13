using System.Collections;

namespace Main.HID
{
    public record Mouse
    {
        public bool LeftButton;
        public bool RightButton;
        public bool MiddleButton;

        public int X;
        public int Y;
        public int Wheel;

        public BitArray ButtonBitArray = new(new[]
        {
                        false, false, false, false, false, false, false
        });
    }
    
    public record Keyboard
    {
    }
}