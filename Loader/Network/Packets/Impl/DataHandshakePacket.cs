using System;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Loader.Utils;

namespace Loader.Network.Packets.Impl
{
    public class DataHandshakePacket : Packet
    {
        public DataHandshakePacket(ServerWrapper server)
        {
            Id = 4;
            Server = server;
        }

        private byte[] _data = Array.Empty<byte>();
        
        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            var dataLength = BitConverter.ToInt32(ReadBuffer(4, buffer));
            var completed = BitConverter.ToBoolean(ReadBuffer(1, buffer));
            
            var pathLength = BitConverter.ToInt32(ReadBuffer(4, buffer));
            var path = Encoding.Unicode.GetString(ReadBuffer(pathLength, buffer));
            
            var mainLength = BitConverter.ToInt32(ReadBuffer(4, buffer));
            var main = Encoding.Unicode.GetString(ReadBuffer(mainLength, buffer));
            
            _data ??= new byte[dataLength];
            
            _data = ByteUtils.CombineArray(_data, ReadBuffer(1500, buffer));
            
            if (completed)
            {
                Array.Resize(ref _data, dataLength);
                AssemblyLoader.LoadAssembly(Assembly.Load(_data), path, main);
            }
        }
    }
}