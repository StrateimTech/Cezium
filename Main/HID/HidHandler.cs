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

        private readonly FileStream _mouseFileStream;
        private readonly FileStream _keyboardFileStream;
        
        private readonly FileStream _hidFileStream;

        public readonly KeyboardApiHandler KeyboardApiHandler;
        public readonly MouseApiHandler MouseApiHandler;

        public readonly HidKeyboardHandler HidKeyboardHandler;
        public readonly HidMouseHandler HidMouseHandler;
        
        private readonly ReaderWriterLockSlim _hidWriteLock = new();
        
        public HidHandler(Settings settings)
        {
            _mouseFileStream = File.Open(MouseStreamPath, FileMode.Open, FileAccess.ReadWrite); 
            _keyboardFileStream = File.Open(KeyboardStreamPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); 
            
            _hidFileStream = File.Open(HumanInterfaceDeviceStreamPath, FileMode.Open, FileAccess.Write);

            KeyboardApiHandler = new(settings);
            MouseApiHandler = new(settings);

            HidKeyboardHandler = new(this, settings, _keyboardFileStream, _hidFileStream);
            HidMouseHandler = new(this, settings, _mouseFileStream, _hidFileStream);
        }

        public void Stop()
        {
            _mouseFileStream.Close();
            _keyboardFileStream.Close();
            _hidFileStream.Close();
        }
        
        public void WriteMouseReport(Mouse mouse, bool leaveOpen = true)    
        {
            _hidWriteLock.EnterWriteLock();
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
                _hidWriteLock.ExitWriteLock();
            }
        }
        
        public void WriteKeyboardReport(Keyboard keyboard, bool leaveOpen = true)
        {
            throw new NotImplementedException();
            // _hidWriteLock.EnterWriteLock();
            // try
            // {
            //     FileUtils.WriteReport(_hidFileStream, Convert.ToSByte(1), new []{BitUtils.ToByte(mouse.ButtonBitArray)}, new []{Convert.ToInt16(mouse.X), Convert.ToInt16(mouse.Y)}, new [] {Convert.ToSByte(mouse.Wheel)}, true);
            // }
            // catch (Exception exception)
            // {
            //     // Good Exception Handling I would say myself :)
            //     ConsoleUtils.WriteCentered($"{exception.Message}");
            // }
            // finally
            // {
            //     _hidWriteLock.ExitWriteLock();
            // }
        }
    }
}