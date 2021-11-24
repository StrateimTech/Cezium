using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Loader.Network.Packets;
using Loader.Network.Packets.Impl;

namespace Loader.Network
{
    public class NetworkHandler
    {
        private TcpClient _client;
        
        public readonly ServerWrapper ServerWrapper = new();
        public PacketHandler PacketHandler;
        public NetworkStream ClientStream;
        
        private bool _disposed;

        public void Connect(string ip, int port)
        { 
            new Task(() =>
            {
                HandleConnection(_client = new TcpClient(ip, port));
            }).Start();
        }

        public void Disconnect()
        {
            _disposed = true;
            _client.Close();
        }

        private void HandleConnection(TcpClient client)
        {
            ClientStream = client.GetStream();
            PacketHandler = new(ServerWrapper);

            ServerWrapper.Connected = true;
            
            /* Init startup */
            PacketHandler.SendPacket(new VersionPacket(ServerWrapper), ClientStream);

            while (!_disposed)
            {
                try
                {
                    if (_disposed)
                    {
                        Console.WriteLine("Client disposed ejecting from loop.");
                        break;
                    }
                    if (!PacketHandler.HandleData(ClientStream))
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex is IOException ? ex.Message : ex.ToString(), GetType().Name);
                    break;
                }
            }
            ServerWrapper.Connected = false;
            Console.WriteLine("Client disconnected.");
            
            ClientStream.Close();
            Thread.CurrentThread.Join();
        }
    }
}