using System.Net.Sockets;
using System.Threading;
using Server.Network.Packets;

namespace Server.Network
{
    public class ClientWrapper
    {
        public ClientWrapper(TcpClient client)
        {
            Client = client;
        }
        
        public readonly TcpClient Client;
        
        /**
         * Latest loader build number
         */
        public static int LoaderBuildNumber = 1;
        
        /**
         * Client's loader build number
         */
        public int ClientBuildNumber = 0;

        public bool Encryption = false;
        public bool Authed = false;
        
        public bool Connected = true;
    }
}