using System;
using System.Text;
using Loader.Network.Packets;

namespace Loader.Utils
{
    public static class PacketUtils
    {
        public static byte[] GetBuffer(Packet packet)
        {
            byte[] output = new byte[1];
            output[0] = (byte) packet.Id;

            foreach (var fieldInfo in packet.GetType().GetFields())
            {
                if(fieldInfo.Name == "Id")
                    continue;
                var value = fieldInfo.GetValue(packet);
                
                var converted = Convert.ChangeType(value, fieldInfo.FieldType);
                if (converted != null)
                {
                    byte[] bytes;
                    if (fieldInfo.FieldType == typeof(byte[]))
                    {
                        bytes = (byte[]) converted;
                    } else if (fieldInfo.FieldType == typeof(string))
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
                        output = ByteUtils.CombineArray(output, bytes);
                    }
                }
            }
            return output;
        }
    }
}