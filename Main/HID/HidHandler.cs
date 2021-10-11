using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Main.Utils;

namespace Main.HID
{
    public class HidHandler
    {
        private readonly Settings _settings;
        private bool _running;
        
        private string MouseStreamPath = "/dev/input/mice";
        
        private string HumanInterfaceDeviceStreamPath = "/dev/hidg0";

        private readonly FileStream _mouseFileStream;
        private readonly FileStream _hidFileStream;

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
        
        private static readonly ReaderWriterLockSlim MouseWriteLock = new();

        public HidHandler(Settings settings)
        {
            _settings = settings;
            _mouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.ReadWrite); 
            
            _hidFileStream = File.Open(HumanInterfaceDeviceStreamPath, FileMode.Open, FileAccess.Write);
        }

        public void Start()
        {
            WriteReport(_mouseFileStream, new byte[] {0xf3, 200, 0xf3, 100, 0xf3, 80});
            _running = true;
            while (_running)
            {
                var mouseSbyteArray = ReadSByteFromStream(_mouseFileStream);
                
                if (mouseSbyteArray.Length > 0)
                {
                    mouseSbyteArray[1] = _settings.General.InvertMouseX ? Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[1]) * -1) : mouseSbyteArray[1];
                    mouseSbyteArray[2] = _settings.General.InvertMouseY ? mouseSbyteArray[2] : Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[2]) * -1);
                    mouseSbyteArray[3] = _settings.General.InvertMouseWheel ? mouseSbyteArray[3] : Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[3]) * -1);
                    
                    LeftButton = (mouseSbyteArray[0] & 0x1) > 0;
                    RightButton = (mouseSbyteArray[0] & 0x2) > 0;
                    MiddleButton = (mouseSbyteArray[0] & 0x4) > 0;
                    X = Convert.ToInt32(mouseSbyteArray[1]);
                    Y = Convert.ToInt32(mouseSbyteArray[2]);
                    Wheel = Convert.ToInt32(mouseSbyteArray[3]);
                    
                    ButtonBitArray = new BitArray(new[]
                    {
                                    LeftButton, RightButton, MiddleButton, false, false, false, false, false
                    });
                    WriteMouseReport(BitUtils.ToByte(ButtonBitArray), new[] {mouseSbyteArray[1], mouseSbyteArray[2], mouseSbyteArray[3]});
                }
            }
        }

        public void Stop()
        {
            _running = false;
            _mouseFileStream.Close();
            _hidFileStream.Close();
        }

        private sbyte[] ReadSByteFromStream(FileStream fileStream, int length = 4)
        {
            var byteArray = new byte[length];
            fileStream.Read(byteArray, 0, byteArray.Length);
            var sbyteArray = new sbyte[byteArray.Length];
            Buffer.BlockCopy(byteArray, 0, sbyteArray, 0, byteArray.Length);
            return sbyteArray;
        }
        
        public void WriteMouseReport(byte button, sbyte[] sbytes, bool leaveOpen = true)
        {
            MouseWriteLock.EnterWriteLock();
            try
            {
                using BinaryWriter binaryWriter = new(_hidFileStream, Encoding.Default, leaveOpen);
                binaryWriter.Write(button);
                foreach (var sByte in sbytes)
                {
                    binaryWriter.Write(sByte);
                }
                binaryWriter.Flush();
            }
            catch (Exception exception)
            {
                ConsoleUtils.WriteCentered($"{exception.Message}");
            }
            finally
            {
                MouseWriteLock.ExitWriteLock();
            }
        }

        private void WriteReport(FileStream fileStream, byte[] bytes, bool leaveOpen = true)
        {
            try
            {
                using BinaryWriter binaryWriter = new(fileStream, Encoding.Default, leaveOpen);
                foreach (var byteE in bytes)
                {
                    binaryWriter.Write(byteE);
                }
                binaryWriter.Flush();
            }
            catch (Exception exception)
            {
                ConsoleUtils.WriteCentered($"{exception.Message}");
            }
        }
    }
}