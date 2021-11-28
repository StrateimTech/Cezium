using System;
using System.Net.Sockets;
using System.Text;
using Server.Utils;

namespace Server.Network.Packets.Impl
{
    public class TestPacket : Packet
    {
        public TestPacket(ClientWrapper client)
        {
            Id = 69;
            Client = client;
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
            ConsoleUtils.WriteLine($"(IP: {Client.Client.Client.RemoteEndPoint}) A: {aV}, B: {bV}, C: {cV}");
            SendPacket(this, clientStream);
        }
    }
}