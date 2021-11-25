using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Server.Network.Packets.Impl;

namespace Server.Network.Packets
{
    public class PacketHandler
    {
        private static ClientWrapper _clientWrapper;
        
        public PacketHandler(ClientWrapper client)
        {
            _clientWrapper = client;
            _packets = new()
            {
                new VersionPacket(_clientWrapper),
                new EncryptionHandshakePacket(_clientWrapper),
                new DataHandshakePacket(_clientWrapper),
                new AuthHandshakePacket(_clientWrapper),
                new TestPacket(_clientWrapper)
            };
        }

        private readonly List<Packet> _packets;

        public bool Handle(ClientWrapper client, NetworkStream clientStream)
        {
            if (!client.Connected)
                return false;
            
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
    }
}