using System.Net.Sockets;

namespace Loader.Network.Packets.Impl
{
    public class VersionPacket : Packet
    {
        public VersionPacket(ServerWrapper server)
        {
            Id = 1;
            Server = server;
        }

        public int buildNumber = Program.Version;
        
        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
        }
    }
}