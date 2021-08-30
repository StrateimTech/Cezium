using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Main.Utils;

namespace Main.API
{
    public class ApiHandler
    {
        private readonly ushort _port;
        private bool _running;
        private Settings _settings;
        
        
        public ApiHandler(ushort port, Settings settings)
        {
            _port = port;
            _settings = settings;
        }

        public void Start()
        {
            _running = true;
            var server = new TcpListener(IPAddress.Any, 200);
            server.Start();
            Console.WriteLine("TCP Listener started.");

            Byte[] bytes = new Byte[256];
            while (_running)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client has connected!");

                NetworkStream stream = client.GetStream();

                try
                {
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // TODO: Implement API once HID stuff is done.
                        // var data = Encoding.ASCII.GetString(bytes, 0, i);
                        // try
                        // {
                        //     var splitData = data.Split(null);
                        //     if (splitData.Length > 0)
                        //     {
                        //         switch (splitData[0])
                        //         {
                        //             case "ChangeState":
                        //                 Boolean.TryParse(splitData[1], out bool value);
                        //                 Console.WriteLine($"Updated state ({_rust.Enabled} -> {value}");
                        //                 _rust.Enabled = value;
                        //                 break;
                        //             case "ChangeFov":
                        //                 Int32.TryParse(splitData[1], out Int32 value2);
                        //                 Console.WriteLine($"Updated fov ({_rust.Fov} -> {value2})");
                        //                 _rust.Fov = value2;
                        //                 _rust.CurrentPixelTable = _rust.CalculatePixelTables(_rust.LoadoutWeapon.Item1);
                        //                 break;
                        //             case "ChangeSens":
                        //                 Double.TryParse(splitData[1], out double value3);
                        //                 Console.WriteLine($"Updated sensitivity ({_rust.Sensitivity} -> {value3})");
                        //                 _rust.Sensitivity = value3;
                        //                 _rust.CurrentPixelTable = _rust.CalculatePixelTables(_rust.LoadoutWeapon.Item1);
                        //                 break;
                        //             case "ChangeGun":
                        //                 Console.WriteLine($"Updated gun ({splitData[1]})");
                        //                 _rust.UpdateGun(splitData[1], splitData[2], splitData[3]);
                        //                 break;
                        //             case "ChangeSmoothness":
                        //                 Int32.TryParse(splitData[1], out Int32 value4);
                        //                 Console.WriteLine($"Updated smoothness ({_rust.Smoothness} -> {value4})");
                        //                 _rust.Smoothness = value4;
                        //                 break;
                        //             default:
                        //                 Console.WriteLine("Unknown Command");
                        //                 break;
                        //         }
                        //     }
                        //     else
                        //     {
                        //         Console.WriteLine("Split data cannot be empty!");
                        //     }
                        // }
                        // catch (Exception exception)
                        // {
                        //     Console.WriteLine($"{exception.Message}");
                        // }

                        // byte[] msg = Encoding.ASCII.GetBytes(data);
                        // stream.Write(msg, 0, msg.Length);
                    }

                    client.Close();
                }
                catch (Exception ex)
                {
                    ConsoleUtils.WriteCentered($"Exception. {ex.Message}");
                }
                finally
                {
                    client.Close();
                }
            }
        }

        public void Stop()
        {
            _running = false;
        }
    }
}