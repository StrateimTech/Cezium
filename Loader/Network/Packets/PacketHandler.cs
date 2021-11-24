using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Loader.Network.Packets.Impl;
using Loader.Utils;

namespace Loader.Network.Packets
{
    public class PacketHandler
    {
        private static ServerWrapper _serverWrapper;
        
        public PacketHandler(ServerWrapper server)
        {
            _serverWrapper = server;
            _packets = new()
            {
                new VersionPacket(_serverWrapper),
                new EncryptionHandshakePacket(_serverWrapper),
                new DataHandshakePacket(_serverWrapper),
                new AuthHandshakePacket(_serverWrapper),
                new TestPacket(_serverWrapper)
            };
        }

        private readonly List<Packet> _packets;

        public bool HandleData(NetworkStream clientStream)
        {
            var buffer = new byte[2048];
            var readDataLength = clientStream.Read(buffer, 0, buffer.Length);
            if (readDataLength > 0)
            {
                foreach (var packet in _packets)
                {
                    if (packet.Id == buffer[0])
                    {
                        var dataBuffer = new byte[2047];
                        Buffer.BlockCopy(buffer, 1, dataBuffer, 0, dataBuffer.Length);
                        packet.Handle(dataBuffer, clientStream);
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        public void SendPacket(Packet packet, NetworkStream clientStream)
        {
            var buffer = PacketUtils.GetBuffer(packet);
            Array.Resize(ref buffer, 2048);
            clientStream.Write(buffer, 0, buffer.Length);
        }
    }
}