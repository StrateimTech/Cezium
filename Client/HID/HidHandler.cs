using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Client.HID.API;
using Client.HID.Handler;
using Client.Utils;

namespace Client.HID
{
    
    public class HidHandler
    {
        private const string MouseStreamPath = "/dev/input/mice";
        private const string KeyboardStreamPath = "/dev/input/by-id/usb-Hoksi_Technology_DGK68C-event-kbd";
        
        private const string HumanInterfaceDeviceStreamPath = "/dev/hidg0";

        private FileStream? _mouseFileStream;
        private FileStream? _keyboardFileStream;
        
        private readonly FileStream _hidFileStream;
        private readonly BinaryWriter _hidBinaryWriter;

        public readonly KeyboardApiHandler KeyboardApiHandler;
        public readonly MouseApiHandler MouseApiHandler;

        public HidKeyboardHandler? HidKeyboardHandler;
        public HidMouseHandler? HidMouseHandler;
        
        private readonly ReaderWriterLockSlim _hidWriteLock = new();
        
        public HidHandler(Settings settings)
        {
            if (File.Exists(MouseStreamPath))
            {
                _mouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.ReadWrite);
            }
            if (File.Exists(KeyboardStreamPath))
            {
                _keyboardFileStream = File.Open(KeyboardStreamPath, FileMode.Open, FileAccess.Read);
            }

            _hidFileStream = File.Open(HumanInterfaceDeviceStreamPath, FileMode.Open, FileAccess.Write);
            _hidBinaryWriter = new BinaryWriter(_hidFileStream, Encoding.Default, true);

            KeyboardApiHandler = new(settings);
            MouseApiHandler = new(settings);

            if (_keyboardFileStream != null)
            {
                HidKeyboardHandler = new(this, settings, _keyboardFileStream);
                ConsoleUtils.WriteLine("Found keyboard stream", "HidHandler");
            }
            else
            {
                ConsoleUtils.WriteLine("Failed to find Keyboard... Continuously Looking...", "HidHandler");
            }

            if (_mouseFileStream != null)
            {
                HidMouseHandler = new(this, settings, _mouseFileStream);
                ConsoleUtils.WriteLine("Found mouse stream", "HidHandler");
            }
            else
            {
                ConsoleUtils.WriteLine("Failed to find Mouse... Continuously Looking...", "HidHandler");
            }

            if (_mouseFileStream == null || _keyboardFileStream == null)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        if (File.Exists(KeyboardStreamPath) && _keyboardFileStream == null)
                        {
                            _keyboardFileStream = File.Open(KeyboardStreamPath, FileMode.Open, FileAccess.Read);
                            HidKeyboardHandler = new(this, settings, _keyboardFileStream);
                            ConsoleUtils.WriteLine("Found keyboard!", "HidHandler");
                        }
                        if (File.Exists(MouseStreamPath) && _mouseFileStream == null)
                        {
                            _mouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.ReadWrite);
                            HidMouseHandler = new(this, settings, _mouseFileStream);
                            ConsoleUtils.WriteLine("Found mouse!", "HidHandler");
                        }
                        if (_mouseFileStream != null && _keyboardFileStream != null)
                            break;
                        Thread.Sleep(1);
                    }
                }).Start();
            }
        }

        public void Stop()
        {
            if (_mouseFileStream != null)
            {
                _mouseFileStream.Close();
            }
            if (_keyboardFileStream != null)
            {
                _keyboardFileStream.Close();
            }
            _hidFileStream.Close();
        }
        
        // TODO: Possible issue here causing rust handler to be thrown off course.
        public void WriteMouseReport(Mouse mouse)
        {
            _hidWriteLock.EnterWriteLock();
            try
            {
                FileUtils.WriteReport(_hidBinaryWriter, 
                    1, 
                    new []{BitUtils.ToByte(mouse.ButtonBitArray)}, 
                    new []{Convert.ToInt16(mouse.X), Convert.ToInt16(mouse.Y)}, 
                    new [] {Convert.ToSByte(mouse.Wheel)});
            }
            catch (Exception exception)
            {
                // Good Exception Handling I would say myself :)
                ConsoleUtils.WriteLine($"{exception.Message}");
            }
            finally
            {
                _hidWriteLock.ExitWriteLock();
            }
        }
        
        public void WriteKeyboardReport(Keyboard keyboard)
        {
            _hidWriteLock.EnterWriteLock();
            try
            {
                byte[] buffer = new byte[8];
                buffer[0] = 2;
                if (keyboard.Modifier != null)
                {
                    buffer[1] = keyboard.Modifier.Value;
                }
                if (keyboard.KeyCode != null)
                {
                    buffer[3] = keyboard.KeyCode.Value;
                }
                
                for (int i = 4; i < 8; i++)
                {
                    buffer[i] = keyboard.ExtraKeys[i-4];
                }
                
                FileUtils.WriteReport(_hidBinaryWriter, buffer);
            }
            catch (Exception exception)
            {
                // Good Exception Handling I would say myself :)
                ConsoleUtils.WriteLine($"{exception.Message}");
            }
            finally
            {
                _hidWriteLock.ExitWriteLock();
            }
        }
    }
}