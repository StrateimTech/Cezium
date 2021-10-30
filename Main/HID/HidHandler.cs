using System;
using System.Buffers.Binary;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Main.HID.API;
using Main.Utils;

namespace Main.HID
{
    
    public class HidHandler
    {
        private readonly Settings _settings;
        private bool _running;
        
        private const string MouseStreamPath = "/dev/input/mice";
        // private const string KeyboardStreamPath = "/dev/input/event0";
        
        private const string HumanInterfaceDeviceStreamPath = "/dev/hidg0";

        private readonly FileStream _mouseFileStream;
        // private readonly FileStream _keyboardFileStream;
        
        private readonly FileStream _hidFileStream;
        
        private readonly ReaderWriterLockSlim _mouseWriteLock = new();
        
        public Mouse Mouse { get; private set; } = new();

        public readonly KeyboardApiHandler KeyboardApiHandler;
        public readonly MouseApiHandler MouseApiHandler;
        
        public HidHandler(Settings settings)
        {
            _settings = settings;
            _mouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.ReadWrite); 
            // _keyboardFileStream = File.Open(KeyboardStreamPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); 
            
            _hidFileStream = File.Open(HumanInterfaceDeviceStreamPath, FileMode.Open, FileAccess.Write);

            KeyboardApiHandler = new KeyboardApiHandler();
            MouseApiHandler = new MouseApiHandler(settings);
        }
        
        
        [StructLayout(LayoutKind.Sequential)]
        private struct InputEvent
        {
            public int Microseconds;
            public ushort type;
            public ushort code;
            public int value;
        }
        
        public void Start()
        {
            FileUtils.WriteReport(_mouseFileStream, new byte[] {0xf3, 200, 0xf3, 100, 0xf3, 80});
            _running = true;
            while (_running)
            {
                // var keyboardSbyteArray = BitUtils.ReadSByteFromStream(_keyboardFileStream, 3);

                // var buffer = new byte[64];
                // var a = _keyboardFileStream.Read(buffer, 0, buffer.Length);
                
                // var ev = new InputEvent[64];
                // var size = Marshal.SizeOf<InputEvent>();
                //
                // var buffer = new byte[size];
                // var a = _keyboardFileStream.Read(buffer, 0, buffer.Length);
                //
                // var cc = ByteArrayToNewStuff(buffer);
                // if (cc != null)
                // {
                //     ConsoleUtils.WriteCentered($"Type {cc.Value.type}");
                //     ConsoleUtils.WriteCentered($"Code {cc.Value.code}");
                //     ConsoleUtils.WriteCentered($"Value {cc.Value.value}\n");
                // }

                if (_settings.General.MouseState)
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
                // if (keyboardSbyteArray.Length > 0)
                // {
                //     ConsoleUtils.WriteCentered($"Report ID: {Convert.ToInt32(keyboardSbyteArray[1])}");
                // }
            }
        }
        
        // private InputEvent? ByteArrayToNewStuff(byte[] bytes)
        // {
        //     GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        //     try
        //     {
        //         return (InputEvent)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(InputEvent));
        //     }
        //     finally
        //     {
        //         handle.Free();
        //     }
        // }

        public void Stop()
        {
            _running = false;
            _mouseFileStream.Close();
            // _keyboardFileStream.Close();
            _hidFileStream.Close();
        }
        
        public void WriteMouseReport(Mouse mouse, bool leaveOpen = true)    
        {
            _mouseWriteLock.EnterWriteLock();
            try
            {
                FileUtils.WriteReport(_hidFileStream, Convert.ToSByte(1), new []{BitUtils.ToByte(mouse.ButtonBitArray)}, new []{Convert.ToInt16(mouse.X), Convert.ToInt16(mouse.Y)}, new [] {Convert.ToSByte(mouse.Wheel)}, true);
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