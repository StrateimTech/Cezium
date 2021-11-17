using System;
using System.Collections;
using System.IO;

namespace Client.Utils
{
    public static class BitUtils
    {
        public static byte ToByte(BitArray bits)
        {
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
        
        public static sbyte[] ReadSByteFromStream(FileStream fileStream, int length = 4)
        {
            var byteArray = new byte[length];
            fileStream.Read(byteArray, 0, byteArray.Length);
            var sbyteArray = new sbyte[byteArray.Length];
            Buffer.BlockCopy(byteArray, 0, sbyteArray, 0, byteArray.Length);
            return sbyteArray;
        }
    }
}