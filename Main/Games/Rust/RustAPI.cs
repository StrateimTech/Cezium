using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Main.Utils;

namespace Main.Games.Rust
{
    public class RustAPI
    {
        
        public RustAPI(RustHandler rustHandler, Settings settings)
        {
            TcpListener? server = null;
            try
            {
                server = new TcpListener(IPAddress.Any, 200);
                server.Start();
                ConsoleUtils.WriteCentered("TCP Listener started.", GetType().Name);

                Byte[] bytes = new Byte[256];
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    ConsoleUtils.WriteCentered("Client has connected!", GetType().Name);

                    NetworkStream stream = client.GetStream();

                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var data = Encoding.ASCII.GetString(bytes, 0, i);

                        try
                        {
                            var splitData = data.Split(null);
                            if (splitData.Length > 0)
                            {
                                switch (splitData[0])
                                {
                                    case "ChangeState":
                                        Boolean.TryParse(splitData[1], out bool value);
                                        ConsoleUtils.WriteCentered($"Updated state ({settings.Rust.State} -> {value}", GetType().Name);
                                        settings.Rust.State = value;
                                        break;
                                    case "ChangeFov":
                                        Int32.TryParse(splitData[1], out Int32 value2);
                                        ConsoleUtils.WriteCentered($"Updated fov ({settings.Rust.Fov} -> {value2})", GetType().Name);
                                        settings.Rust.Fov = value2;
                                        rustHandler.PixelTable = rustHandler.CalculatePixelTables(rustHandler.CurrentWeapon.Item2);
                                        break;
                                    case "ChangeSens":
                                        Double.TryParse(splitData[1], out double value3);
                                        ConsoleUtils.WriteCentered($"Updated sensitivity ({settings.Rust.Sensitivity} -> {value3})", GetType().Name);
                                        settings.Rust.Sensitivity = value3;
                                        rustHandler.PixelTable = rustHandler.CalculatePixelTables(rustHandler.CurrentWeapon.Item2);
                                        break;
                                    case "ChangeGun":
                                        ConsoleUtils.WriteCentered($"Updated gun ({splitData[1]})", GetType().Name);
                                        rustHandler.UpdateGun(splitData[1], splitData[2], splitData[3]);
                                        break;
                                    case "ChangeSmoothness":
                                        Int32.TryParse(splitData[1], out Int32 value4);
                                        ConsoleUtils.WriteCentered($"Updated smoothness ({settings.Rust.Smoothness} -> {value4})", GetType().Name);
                                        settings.Rust.Smoothness = value4;
                                        break;
                                    default:
                                        ConsoleUtils.WriteCentered("Unknown Command", GetType().Name);
                                        break;
                                }
                            }
                            else
                            {
                                ConsoleUtils.WriteCentered("Split data cannot be empty!", GetType().Name);
                            }
                        }
                        catch (Exception exception)
                        {
                            ConsoleUtils.WriteCentered($"{exception.Message}", GetType().Name);
                        }

                        byte[] msg = Encoding.ASCII.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);
                    }

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                ConsoleUtils.WriteCentered($"SocketException: {e}", GetType().Name);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}