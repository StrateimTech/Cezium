using System;
using System.Net.Sockets;

namespace Server.Network.Packets.Impl
{
    public class EncryptionHandshakePacket : Packet
    {
        public EncryptionHandshakePacket(ClientWrapper client)
        {
            Id = 2;
            Client = client;
        }
        
        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            throw new NotImplementedException();
        }
    }
}