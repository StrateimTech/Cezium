using System;
using System.Net.Sockets;
using System.Text;
using Server.Utils;

namespace Server.Network.Packets.Impl
{
    public class VersionPacket : Packet
    {
        public VersionPacket()
        {
            Id = 1;
        }

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            var versionNumber = BitConverter.ToInt32(ReadBuffer(4, buffer));
            var versionString = Encoding.UTF8.GetString(ReadBuffer(32, buffer));
            ConsoleUtils.WriteLine($"VersionNumber: {versionNumber} | VersionString {versionString}");
            SendPacket(GetBuffer(new EncryptionHandshakePacket()), clientStream);
        }
    }
}