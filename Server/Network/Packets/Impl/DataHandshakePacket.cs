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
            
            ConsoleUtils.WriteLine($"(IP: {Client.Client.Client.RemoteEndPoint}) Client requested client data (IsAuthed: {Client.Authed})", "NetworkHandler");
            
            if(!Client.Authed)
                return;

            byte[][] splitData;
            string localPath = "Program";
            string localMain = "Main";
            int assemblyLength;
            if (Program.Settings.Obfuscation)
            {
                var obfuscationHandler = new ObfuscationHandler(Program.ClientAssembly);
                var obfuscatedAssembly = obfuscationHandler.Run();
                
                localPath = obfuscatedAssembly.Item2;
                localMain = obfuscatedAssembly.Item3;

                assemblyLength = obfuscatedAssembly.Item1.Length;
                
                splitData = ByteUtils.BufferSplit(obfuscatedAssembly.Item1, 750);
            }
            else
            {
                assemblyLength = Program.ClientAssembly.Length;
                
                splitData = ByteUtils.BufferSplit(Program.ClientAssembly, 750);
            }
            
            for (int i = 0; i < splitData.Length; i++)
            { 
                SendPacket(new DataHandshakePacket(Client)
                {
                    data = splitData[i],
                    length = assemblyLength,
                    completed = i == splitData.Length - 1,
                    pathLength = Encoding.Unicode.GetBytes(localPath).Length,
                    path = localPath,
                    mainLength = Encoding.Unicode.GetBytes(localMain).Length,
                    main = localMain
                }, clientStream);
                Thread.Sleep(50);
            }
            ConsoleUtils.WriteLine($"(IP: {Client.Client.Client.RemoteEndPoint}) Client's requested data has fully sent (IsAuthed: {Client.Authed}, Obfuscation: {Program.Settings.Obfuscation})", "NetworkHandler");
        }
    }
}