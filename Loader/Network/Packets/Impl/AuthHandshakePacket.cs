﻿using System;
using System.Net.Sockets;
using Loader.Utils;

namespace Loader.Network.Packets.Impl
{
    public class AuthHandshakePacket : Packet
    {
        public AuthHandshakePacket(ServerWrapper server)
        {
            Id = 3;
            Server = server;
        }
        
        public int AccountId;
        
        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            var status = BitConverter.ToInt32(ReadBuffer(4, buffer));
            if (status == 1)
            {
                ConsoleUtils.WriteLine("Successfully logged in! Loading cezium client...");
                Server.Authed = true;
                SendPacket(new DataHandshakePacket(Server), clientStream);
            }
            else
            {
                ConsoleUtils.WriteLine("Failed to login. (Restart)");
            }
        }
    }
}