﻿using System;
using System.Net.Sockets;
using System.Threading;
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
        
        public bool Status;

        public override void Handle(byte[] buffer, NetworkStream clientStream)
        {
            base.Handle(buffer, clientStream);
            var accountId = BitConverter.ToInt32(ReadBuffer(4, buffer));
            
            if (accountId == 2424)
            {
                Status = true;
                Client.Authed = true;
            }
            ConsoleUtils.WriteLine($"{(Program.Settings.HideIp ? "" : $"(IP: {Client.Client.Client.RemoteEndPoint}) ")}Client authentication received (IsAuthed: {Client.Authed} | Status: {Status} | AccountID: {accountId})", "NetworkHandler");
            SendPacket(this, clientStream);
        }
    }
}