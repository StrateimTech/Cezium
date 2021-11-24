using System;
using System.Net.Sockets;

namespace Loader.Network.Packets.Impl
{
    public class EncryptionHandshakePacket : Packet
    {
        public EncryptionHandshakePacket(ServerWrapper server)
        {
            Id = 2;
            Server = server;
        }

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            throw new NotImplementedException();
        }
    }
}