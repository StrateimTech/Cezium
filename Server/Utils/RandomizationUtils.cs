using System;

namespace Server.Utils
{
    public static class RandomizationUtils
    {
        private static readonly Random Random = new();
        
        public static string RandomString(int length)
        {
            byte[] bytes = new byte[length];
            Random.NextBytes(bytes);
            return GetString(bytes);
        }
        
        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}