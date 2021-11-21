using System;
using System.Net.Sockets;

namespace Server.Network.Packets.Impl
{
    public class EncryptionHandshakePacket : Packet
    {
        public EncryptionHandshakePacket()
        {
            Id = 2;
        }

        public string a = "lol";
        public int b = 123;
        public bool c = true;

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
        }
    }
}