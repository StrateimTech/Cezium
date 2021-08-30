using System.Collections;

namespace Main.Utils
{
    public static class BitUtils
    {
        public static byte ToByte(BitArray bits)
        {
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}