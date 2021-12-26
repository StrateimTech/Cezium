using System;
using System.Net.Sockets;
using Loader.Utils;

namespace Loader.Network.Packets.Impl
{
    public class VersionPacket : Packet
    {
        public VersionPacket(ServerWrapper server)
        {
            Id = 1;
            Server = server;
        }

        public int BuildNumber = Program.Version;
        
        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            var loaderVersion = BitConverter.ToInt32(ReadBuffer(4, buffer));
            ConsoleUtils.WriteLine(loaderVersion > BuildNumber
                ? $"Loader outdated (Current build: {BuildNumber}, Newest build: {loaderVersion})"
                : $"Loader up to date ({BuildNumber})");
        }
    }
}