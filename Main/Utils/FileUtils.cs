using System.IO;
using System.Text;

namespace Main.Utils
{
    public static class FileUtils
    {
        public static void WriteReport(FileStream fileStream, byte[] bytes, bool leaveOpen = true)
        {
            using BinaryWriter binaryWriter = new(fileStream, Encoding.Default, leaveOpen);
            foreach (var unsignedByte in bytes)
            {
                binaryWriter.Write(unsignedByte);
            }
            binaryWriter.Flush();
        }
        
        public static void WriteReport(FileStream fileStream, sbyte[] sbytes, bool leaveOpen = true)
        {
            using BinaryWriter binaryWriter = new(fileStream, Encoding.Default, leaveOpen);
            foreach (var signedByte in sbytes)
            {
                binaryWriter.Write(signedByte);
            }
            binaryWriter.Flush();
        }
        
        public static void WriteReport(FileStream fileStream, byte[] bytes, sbyte[] sbytes, bool leaveOpen = true)
        {
            using BinaryWriter binaryWriter = new(fileStream, Encoding.Default, leaveOpen);
            foreach (var unsignedByte in bytes)
            {
                binaryWriter.Write(unsignedByte);
            }
            foreach (var signedByte in sbytes)
            {
                binaryWriter.Write(signedByte);
            }
            binaryWriter.Flush();
        }
    }
}