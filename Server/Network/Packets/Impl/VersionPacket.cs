using System;
using System.Net.Sockets;
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
            var buildNumber = BitConverter.ToInt32(ReadBuffer(4, buffer));
            Client.BuildNumber = buildNumber;
            ConsoleUtils.WriteLine($"(IP: {Client.Client.Client.RemoteEndPoint}) Client connected using loader build {buildNumber}", "NetworkHandler");
        }
    }
}