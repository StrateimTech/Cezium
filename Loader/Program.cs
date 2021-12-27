﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Figgle;
using Loader.Network;
using Loader.Network.Packets.Impl;
using Loader.Utils;

namespace Loader
{
    class Program
    {
        public static readonly int Version = 1;
        public static string[] ClientArguments;

        public static string[] Servers = {
            "66.94.123.254"
        };
        
        private static void Main(string[] args)
        {
            ClientArguments = args;
            
            var figgleText = FiggleFonts.Isometric3.Render("CEZIUM");
            var figgleLines = Regex.Split(figgleText, "\r\n|\r|\n");
            foreach (var figgleLine in figgleLines)
            {
                ConsoleUtils.WriteCentered(figgleLine);
            }
            ConsoleUtils.WriteLine("Project by StrateimTech (https://strateim.tech)");
            
            #if RELEASE 
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ConsoleUtils.WriteLine($"Platform unsupported please use Raspbian or linux alternative. ({Environment.OSVersion})");
                return;
            }
            #endif
            
            ConsoleUtils.WriteLine("Attempting to connect to a parent server");
            var networkHandler = new NetworkHandler();
            if (!networkHandler.Connect(Servers[0], 3000))
            {
                ConsoleUtils.WriteLine("Couldn't establish connection to parent server (Retry)");
                return;
            }
            ConsoleUtils.WriteLine("Successfully established connection to a parent server!");

            while (true)
            {
                if (networkHandler.ServerWrapper.VersionSynced != null)
                {
                    if (networkHandler.ServerWrapper.VersionSynced == true)
                    {
                        break;
                    }
                    return;
                }
                Thread.Sleep(1);
            }
            
            ConsoleUtils.WriteLine("Please login using your account ID:");
            Console.CursorLeft = 1;
            var outputId = Console.ReadLine();
            Int32.TryParse(outputId, out int id);
            
            if (outputId == null)
            {
                ConsoleUtils.WriteLine("Couldn't understand account Id please restart the application.");
                networkHandler.Disconnect();
                return;
            }
            
            if (networkHandler.ServerWrapper.Connected)
            {
                ConsoleUtils.WriteLine("Logging in please wait...");
                networkHandler.PacketHandler.SendPacket(new AuthHandshakePacket(networkHandler.ServerWrapper)
                {
                    AccountId = id
                }, networkHandler.ClientStream);
            }
            else
            {
                ConsoleUtils.WriteLine("Couldn't establish data connection to the parent server please restart the application");
                networkHandler.Disconnect();
                return;
            }
            
            Console.ReadKey(true);
            ConsoleUtils.WriteLine("Shutting down...");
            networkHandler.Disconnect();
            Environment.Exit(0);
        }
    }
}