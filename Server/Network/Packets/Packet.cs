using System;
using System.Net.Sockets;
using Server.Utils;

namespace Server.Network.Packets
{
    public abstract class Packet
    {
        protected static ClientWrapper Client;
        public int Id = 0;

        private int _offset;

        public virtual void Handle(byte[] buffer, NetworkStream clientStream)
        {
            _offset = 0;
        }

        protected byte[] ReadBuffer(int length, byte[] buffer)
        {
            var output = new byte[length];
            for (int i = 0; i < length; i++)
            {
                output[i] = buffer[_offset + i];
            }
            _offset =+ length;
            return output;
        }

        protected void SendPacket(Packet packet, NetworkStream clientStream)
        {
            var buffer = PacketUtils.GetBuffer(packet);
            Array.Resize(ref buffer, 2048);
            clientStream.Write(buffer, 0, buffer.Length);
        }
    }
}