using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Server.Network.Packets.Impl;

namespace Server.Network.Packets
{
    public class PacketHandler
    {
        public PacketHandler(ClientWrapper client)
        {
            _packets = new()
            {
                new VersionPacket(client),
                new EncryptionHandshakePacket(client),
                new DataHandshakePacket(client),
                new AuthHandshakePacket(client),
                new TestPacket(client)
            };
        }

        private readonly List<Packet> _packets;

        public bool Handle(ClientWrapper client, NetworkStream clientStream)
        {
            if (!client.Connected)
                return false;
            var buffer = new byte[1024];
            var readDataLength = clientStream.Read(buffer, 0, buffer.Length);
            if (readDataLength > 0)
            {
                foreach (var packet in _packets)
                {
                    if (packet.Id == buffer[0])
                    {
                        var dataBuffer = new byte[1023];
                        Buffer.BlockCopy(buffer, 1, dataBuffer, 0, dataBuffer.Length);
                        packet.Handle(dataBuffer, clientStream);
                        break;
                    }
                }
                return true;
            }
            return false;
        }
    }
}