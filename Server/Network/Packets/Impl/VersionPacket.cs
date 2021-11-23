using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Server.Utils;

namespace Server.Network.Packets.Impl
{
    public class VersionPacket : Packet
    {
        public VersionPacket(ClientWrapper client)
        {
            Id = 1;
            Client = client;
        }

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            var versionNumber = BitConverter.ToInt32(ReadBuffer(4, buffer));
            var versionString = Encoding.Unicode.GetString(ReadBuffer(32, buffer));
            ConsoleUtils.WriteLine($"VersionNumber: {versionNumber} | VersionString {versionString}");
        }
    }
}