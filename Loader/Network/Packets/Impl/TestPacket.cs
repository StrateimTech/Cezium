using System;
using System.Net.Sockets;
using System.Text;

namespace Loader.Network.Packets.Impl
{
    public class TestPacket : Packet
    {
        public TestPacket(ServerWrapper server)
        {
            Id = 69;
            Server = server;
        }

        public string a = "Some string data";
        public int b = 291829218;
        public bool c = true;
        
        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            var aV = Encoding.Unicode.GetString(ReadBuffer(32, buffer));
            var bV = BitConverter.ToInt32(ReadBuffer(4, buffer));
            var cV = BitConverter.ToBoolean(ReadBuffer(1, buffer));
            Console.WriteLine($"A: {aV}, B: {bV}, C: {cV}");
        }
    }
}