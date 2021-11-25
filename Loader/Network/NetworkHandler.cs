using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Loader.Network.Packets;
using Loader.Network.Packets.Impl;
using Loader.Utils;

namespace Loader.Network
{
    public class NetworkHandler
    {
        private TcpClient _client;
        
        public readonly ServerWrapper ServerWrapper = new();
        public PacketHandler PacketHandler;
        public NetworkStream ClientStream;
        
        private bool _disposed;

        public bool Connect(string ip, int port)
        {
            try
            {
                _client = new TcpClient(ip, port);
            }
            catch (Exception ex)
            {
                ConsoleUtils.WriteLine($"{ex.Message}");
                return false;
            }
            new Thread(() => HandleConnection(_client)).Start();
            return true;
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
                    if (!PacketHandler.HandleData(ClientStream))
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleUtils.WriteLine(ex is IOException ? ex.Message : ex.ToString());
                    break;
                }
            }
            ServerWrapper.Connected = false;
            ConsoleUtils.WriteLine("Disconnected from parent server.");
            
            ClientStream.Close();
            Thread.CurrentThread.Join();
        }
    }
}