using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Server.Obfuscation;
using Server.Utils;

namespace Server.Network.Packets.Impl
{
    public class DataHandshakePacket : Packet
    {
        public DataHandshakePacket(ClientWrapper client)
        {
            Id = 4;
            Client = client;
        }

        public int length;
        public bool completed;
        
        public int pathLength;
        public string path;
        
        public int mainLength;
        public string main;
        
        public byte[] data;

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            
            if(!Client.Authed)
                return;
            
            var obfuscationHandler = new ObfuscationHandler(Program.ClientAssembly);
            var obfuscatedAssembly = obfuscationHandler.Run();
            
            var localPath = obfuscatedAssembly.Item2;
            var localMain = obfuscatedAssembly.Item3;
            
            var splitData = ByteUtils.BufferSplit(obfuscatedAssembly.Item1, 1500);
            for (int i = 0; i < splitData.Length; i++)
            {
                SendPacket(new DataHandshakePacket(Client)
                {
                    data = splitData[i],
                    length = obfuscatedAssembly.Item1.Length,
                    completed = i == splitData.Length - 1,
                    pathLength = Encoding.Unicode.GetBytes(localPath).Length,
                    path = localPath,
                    mainLength = Encoding.Unicode.GetBytes(localMain).Length,
                    main = localMain
                }, clientStream);
                Thread.Sleep(1);
            }
        }
    }
}