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
        
        public static void WriteReport(FileStream fileStream, sbyte reportId, byte[] bytes, short[] shorts, sbyte[] sbytes, bool leaveOpen = true)
        {
            using BinaryWriter binaryWriter = new(fileStream, Encoding.Default, leaveOpen);
            binaryWriter.Write(reportId);
            foreach (var unsignedByte in bytes)
            {
                binaryWriter.Write(unsignedByte);
            }
            foreach (var signedUShort in shorts)
            {
                binaryWriter.Write(signedUShort);
            }
            foreach (var signedByte in sbytes)
            {
                binaryWriter.Write(signedByte);
            }
            binaryWriter.Flush();
        }
        
    }
}