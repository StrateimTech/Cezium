﻿using System;
using System.Net.Sockets;
using System.Text;
using Server.Utils;

namespace Server.Network.Packets.Impl
{
    public class AuthHandshakePacket : Packet
    {
        public AuthHandshakePacket(ClientWrapper client)
        {
            Id = 3;
            Client = client;
        }
        
        public int Status;

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            var accountId = BitConverter.ToInt32(ReadBuffer(4, buffer));
            
            if (accountId == 2424)
            {
                Status = 1;
                Client.Authed = true;
            }
            SendPacket(this, clientStream);
        }
    }
}