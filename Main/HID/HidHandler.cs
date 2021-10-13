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
        
        private const string MouseStreamPath = "/dev/input/mice";
        private const string HumanInterfaceDeviceStreamPath = "/dev/hidg0";

        private readonly FileStream _mouseFileStream;
        private readonly FileStream _hidFileStream;
        
        private readonly ReaderWriterLockSlim _mouseWriteLock = new();
        
        public Mouse Mouse { get; private set; } = new();

        public HidHandler(Settings settings)
        {
            _settings = settings;
            _mouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.ReadWrite); 
            
            _hidFileStream = File.Open(HumanInterfaceDeviceStreamPath, FileMode.Open, FileAccess.Write);
        }

        public void Start()
        {
            FileUtils.WriteReport(_mouseFileStream, new byte[] {0xf3, 200, 0xf3, 100, 0xf3, 80});
            _running = true;
            while (_running)
            {
                var mouseSbyteArray = BitUtils.ReadSByteFromStream(_mouseFileStream);
                if (mouseSbyteArray.Length > 0)
                {
                    mouseSbyteArray[1] = _settings.General.InvertMouseX ? Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[1]) * -1) : mouseSbyteArray[1];
                    mouseSbyteArray[2] = _settings.General.InvertMouseY ? mouseSbyteArray[2] : Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[2]) * -1);
                    mouseSbyteArray[3] = _settings.General.InvertMouseWheel ? mouseSbyteArray[3] : Convert.ToSByte(Convert.ToInt32(mouseSbyteArray[3]) * -1);
                    
                    WriteMouseReport(Mouse = new Mouse()
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

        public void Stop()
        {
            _running = false;
            _mouseFileStream.Close();
            _hidFileStream.Close();
        }
        
        public void WriteMouseReport(Mouse mouse, bool leaveOpen = true)
        {
            _mouseWriteLock.EnterWriteLock();
            try
            {
                FileUtils.WriteReport(_hidFileStream, new []{BitUtils.ToByte(mouse.ButtonBitArray)}, new []{Convert.ToSByte(mouse.X), Convert.ToSByte(mouse.Y), Convert.ToSByte(mouse.Wheel)}, leaveOpen);
            }
            catch (Exception exception)
            {
                // Good Exception Handling I would say myself :)
                ConsoleUtils.WriteCentered($"{exception.Message}");
            }
            finally
            {
                _mouseWriteLock.ExitWriteLock();
            }
        }
    }
}