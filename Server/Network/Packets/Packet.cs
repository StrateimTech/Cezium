using System;
using System.Net.Sockets;
using System.Text;
using Server.Utils;

namespace Server.Network.Packets
{
    public abstract class Packet
    {
        public int Id = 0;

        private int _offset;

        public abstract void Handle(byte[] buffer, NetworkStream clientStream);

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

        protected void SendPacket(byte[] buffer, NetworkStream clientStream)
        {
            clientStream.Write(buffer, 0, buffer.Length);
        }

        protected byte[] GetBuffer(Packet packet)
        {
            byte[] output = new byte[256];
            output[0] = (byte) packet.Id;

            foreach (var fieldInfo in packet.GetType().GetFields())
            {
                if(fieldInfo.Name == "Id")
                    continue;
                ConsoleUtils.WriteLine($"Name: {fieldInfo.Name} Type: {fieldInfo.FieldType.Name}");
                var value = fieldInfo.GetValue(packet);
                ConsoleUtils.WriteLine($"{value}");
                
                var converted = Convert.ChangeType(value, fieldInfo.FieldType);
                if (converted != null)
                {
                    byte[] bytes;
                    if (fieldInfo.FieldType == typeof(string))
                    {
                        bytes = Encoding.Unicode.GetBytes((string)converted);
                    }
                    else
                    {
                        bytes = (byte[])typeof(BitConverter).GetMethod("GetBytes", new [] {fieldInfo.FieldType})
                            ?.Invoke(null,new []{converted});
                    }
                    if (bytes != null)
                    {
                        ConsoleUtils.WriteLine($"Length: {bytes.Length}");
                        output = ByteUtils.CombineArray(output, bytes);
                    }
                }
            }
            return output;
        }
    }
}