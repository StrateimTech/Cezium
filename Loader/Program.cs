using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Loader.Network;
using Loader.Network.Packets.Impl;

namespace Loader
{
    class Program
    {
        private static void Main(string[] args)
        {
            // if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            // {
                // throw new Exception($"Platform not supported ({Environment.OSVersion})");
            // }
            
            var networkHandler = new NetworkHandler();
            networkHandler.Connect("127.0.0.1", 3000);
            
            Console.WriteLine("Enter Account Id: ");
            var outputId = Console.ReadLine();
            Int32.TryParse(outputId, out int id);
            
            if (outputId == null)
            {
                Console.WriteLine("Account ID is null");
                networkHandler.Disconnect();
                return;
            }
            
            if (networkHandler.ServerWrapper.Connected)
            {
                networkHandler.PacketHandler.SendPacket(new AuthHandshakePacket(networkHandler.ServerWrapper)
                {
                    AccountId = id
                }, networkHandler.ClientStream);
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            networkHandler.Disconnect();
            // byte[] fileBytes = File.ReadAllBytes(args[0]);
            // Console.WriteLine($"File Size: {fileBytes.Length}");
            //
            // // LoadAssembly(Assembly.Load(fileBytes));
            // Array.Clear(fileBytes, 0, fileBytes.Length);
        }
    }
}