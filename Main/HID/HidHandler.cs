using System;
using System.Buffers.Binary;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Main.HID.API;
using Main.HID.Handler;
using Main.Utils;

namespace Main.HID
{
    
    public class HidHandler
    {
        private const string MouseStreamPath = "/dev/input/mice";
        private const string KeyboardStreamPath = "/dev/input/event0";
        
        private const string HumanInterfaceDeviceStreamPath = "/dev/hidg0";

        private readonly FileStream? _mouseFileStream;
        private readonly FileStream? _keyboardFileStream;
        
        private readonly FileStream _hidFileStream;

        public readonly KeyboardApiHandler KeyboardApiHandler;
        public readonly MouseApiHandler MouseApiHandler;

        public readonly HidKeyboardHandler? HidKeyboardHandler;
        public readonly HidMouseHandler? HidMouseHandler;
        
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

            KeyboardApiHandler = new(settings);
            MouseApiHandler = new(settings);

            if (_keyboardFileStream != null)
            {
                HidKeyboardHandler = new(this, settings, _keyboardFileStream);
            }
            else
            {
                ConsoleUtils.WriteCentered("Failed to find Keyboard", "HidHandler");
            }

            if (_mouseFileStream != null)
            {
                HidMouseHandler = new(this, settings, _mouseFileStream);
            }
            else
            {
                ConsoleUtils.WriteCentered("Failed to find Mouse", "HidHandler");
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
        
        public void WriteMouseReport(Mouse mouse, bool leaveOpen = true)    
        {
            _hidWriteLock.EnterWriteLock();
            try
            {
                FileUtils.WriteReport(_hidFileStream, 1, new []{BitUtils.ToByte(mouse.ButtonBitArray)}, new []{Convert.ToInt16(mouse.X), Convert.ToInt16(mouse.Y)}, new [] {Convert.ToSByte(mouse.Wheel)}, true);
            }
            catch (Exception exception)
            {
                // Good Exception Handling I would say myself :)
                ConsoleUtils.WriteCentered($"{exception.Message}");
            }
            finally
            {
                _hidWriteLock.ExitWriteLock();
            }
        }
        
        public void WriteKeyboardReport(Keyboard keyboard, bool leaveOpen = true)
        {
            _hidWriteLock.EnterWriteLock();
            try
            {
                using BinaryWriter binaryWriter = new(_hidFileStream, Encoding.Default, leaveOpen);
                byte[] buffer = new byte[9];
                buffer[0] = 2;
                if (keyboard.Modifier != null)
                {
                    buffer[1] = keyboard.Modifier.Value;
                }
                if (keyboard.KeyCode != null)
                {
                    buffer[3] = keyboard.KeyCode.Value;
                }

                buffer[4] = keyboard.ExtraKeys[0];
                buffer[5] = keyboard.ExtraKeys[1];
                buffer[6] = keyboard.ExtraKeys[2];
                buffer[7] = keyboard.ExtraKeys[3];
                buffer[8] = keyboard.ExtraKeys[4];

                binaryWriter.Write(buffer);
                binaryWriter.Flush();
            }
            catch (Exception exception)
            {
                // Good Exception Handling I would say myself :)
                ConsoleUtils.WriteCentered($"{exception.Message}");
            }
            finally
            {
                _hidWriteLock.ExitWriteLock();
            }
        }
    }
}