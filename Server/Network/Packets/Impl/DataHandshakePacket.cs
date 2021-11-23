using System.IO;
using System.Net.Sockets;
using System.Threading;
using Server.Utils;

namespace Server.Network.Packets.Impl
{
    public class DataHandshakePacket : Packet
    {
        public DataHandshakePacket(ClientWrapper client)
        {
            Id = 3;
            Client = client;
        }

        public int length;
        public bool completed;
        public byte[] data;

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            byte[] fileAssemblyBytes = File.ReadAllBytes(@"E:\Programming\Projects\StrateimTech\Cezium\Client\publish\Client.dll");
            
            var splitData = ByteUtils.BufferSplit(fileAssemblyBytes, 2000);
            
            for (int i = 0; i < splitData.Length; i++)
            {
                SendPacket(new DataHandshakePacket(Client)
                {
                    data = splitData[i],
                    length = fileAssemblyBytes.Length,
                    completed = i == splitData.Length - 1
                }, clientStream);
                Thread.Sleep(1);
            }
        }
    }
}