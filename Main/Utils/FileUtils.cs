using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Main.Utils
{
    public static class FileUtils
    {
        private static readonly ReaderWriterLockSlim MouseWriteLock = new();
        private static readonly ReaderWriterLockSlim KeyboardWriteLock = new();

        public static void write_keyboard_report(FileStream fileStream)
        {
            // TODO: Implement Keyboard Interface. 
        }
        
        public static void write_mouse_report(FileStream fileStream, byte button, sbyte[] sbytes, bool leaveOpen = true)
        {
            MouseWriteLock.EnterWriteLock();
            try
            {
                using BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.Default, leaveOpen);
                binaryWriter.Write(button);
                foreach (var sByte in sbytes)
                {
                    binaryWriter.Write(sByte);
                }
                binaryWriter.Flush();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
            finally
            {
                MouseWriteLock.ExitWriteLock();
            }
        }
    }
}