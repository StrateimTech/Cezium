using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using dnlib.DotNet;
using Server.Network.Packets.Impl;
using Server.Network.Wrappers;
using Server.Utils;

namespace Server.Network.Packets
{
    public class PacketHandler
    {
        private readonly List<Packet> _packets = new()
        {
            new VersionPacket()
        };


        public bool Handle(ClientWrapper client, NetworkStream clientStream)
        {
            if (!client.Connected)
                return false;
            var buffer = new byte[256];
            var readDataLength = clientStream.Read(buffer, 0, buffer.Length);
            if (readDataLength > 0)
            {
                foreach (var packet in _packets)
                {
                    if (packet.Id == buffer[0])
                    {
                        var dataBuffer = new byte[255];
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