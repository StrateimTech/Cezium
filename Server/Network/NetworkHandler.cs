using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Server.Network.Packets;
using Server.Utils;

namespace Server.Network
{
    public class NetworkHandler
    {
        private TcpListener _serverListener;
        
        private bool _disposed;
        
        public void Start(int port)
        {
            ConsoleUtils.WriteLine("Binding TCPListener to Port", GetType().Name);
            (_serverListener = new TcpListener(IPAddress.Any, port)).Start();
            while (_serverListener.Server.IsBound && !_disposed)
            {
                try
                {
                    if (!_serverListener.Pending())
                    {
                        Thread.Sleep(1);
                        continue;
                    }

                    TcpClient client = _serverListener.AcceptTcpClient();
                    ConsoleUtils.WriteLine($"Accepted new TCP Connection (IP: {client.Client.RemoteEndPoint})", GetType().Name);
                    new Task(() =>
                    {
                        HandleClient(client);
                    }).Start();
                }
                catch (Exception ex)
                {
                    //TODO: Improve exception handling this could be very useful in certain situations (EG. Someone attempting to exploit the backend)
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Stop()
        {
            _disposed = true;
            if (_serverListener != null)
            {
                _serverListener.Stop();
            }
        }

        private void HandleClient(TcpClient client)
        {
            var clientWrapper = new ClientWrapper();
            var packetHandler = new PacketHandler(clientWrapper);
            var clientStream = client.GetStream();
            
            while (clientWrapper.Connected)
            {
                try
                {
                    if (!packetHandler.Handle(clientWrapper, clientStream))
                    {
                        clientWrapper.Connected = false;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleUtils.WriteLine(ex is IOException ? ex.Message : ex.ToString(), GetType().Name);
                    if (ex is IOException)
                    {
                        clientWrapper.Connected = false;
                    }
                    break;
                }
            }
            ConsoleUtils.WriteLine("Client disconnect.", GetType().Name);
            
            client.Dispose();
        }
    }
}