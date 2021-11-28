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

        public TcpClient Client;

        public int BuildNumber = 0;

        public bool Encryption = false;
        public bool Authed = false;
        
        public bool Connected = true;
        
    }
}