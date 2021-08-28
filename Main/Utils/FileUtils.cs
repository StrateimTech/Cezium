using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Main.Utils
{
    public class FileUtils
    {
        private readonly ReaderWriterLockSlim _mouseWriteLock = new();
        private readonly ReaderWriterLockSlim _keyboardWriteLock = new();

        public void write_keyboard_report(FileStream fileStream)
        {
            // TODO: Implement Keyboard Interface. 
        }
        
        public void write_mouse_report(FileStream fileStream, byte button, sbyte[] sbytes, bool leaveOpen = true)
        {
            _mouseWriteLock.EnterWriteLock();
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
                _mouseWriteLock.ExitWriteLock();
            }
        }
    }
}