﻿using System;
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
                ConsoleUtils.WriteLine($"{exception.Message}");
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
                ConsoleUtils.WriteLine($"{exception.Message}");
            }
            finally
            {
                _hidWriteLock.ExitWriteLock();
            }
        }
    }
}