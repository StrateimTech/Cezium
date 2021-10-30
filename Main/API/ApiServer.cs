using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Main.HID;
using Main.Rust;
using Main.Utils;

namespace Main.API
{
    public class ApiServer
    {

        public ApiServer(int port, RustHandler rustHandler, HidHandler hidHandler)
        {
            ApiHandler apiHandler = new(rustHandler, hidHandler);
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Byte[] bytes = new Byte[256];
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                
                int i;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    var data = Encoding.ASCII.GetString(bytes, 0, i);
                    apiHandler.HandlePacket(data.Split(null));
                    ConsoleUtils.WriteCentered(data);
                    stream.Write(bytes, 0, bytes.Length);
                }
                client.Close();
            }
        }
    }
}