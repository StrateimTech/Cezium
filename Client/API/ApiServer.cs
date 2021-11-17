using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Client.HID;
using Client.Rust;

namespace Client.API
{
    public class ApiServer
    {

        public ApiServer(int port, RustHandler rustHandler, HidHandler hidHandler, Settings settings)
        {
            ApiHandler apiHandler = new(rustHandler, hidHandler, settings);
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
                    var receiveData = Encoding.ASCII.GetString(bytes, 0, i);
                    var sendData = apiHandler.HandlePacket(receiveData.Split(null));
                    // ConsoleUtils.WriteCentered($"Incoming: {receiveData} | Outgoing: {receiveData}");
                    if (sendData != null)
                    {
                        var sendBytes = Encoding.ASCII.GetBytes(sendData);
                        stream.Write(sendBytes, 0, sendBytes.Length);
                    }
                    else
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
                client.Close();
            }
        }
    }
}