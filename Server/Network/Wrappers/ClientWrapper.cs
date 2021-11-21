using System.Net.Sockets;

namespace Server.Network.Wrappers
{
    public class ClientWrapper
    {
        public ClientWrapper(TcpClient client)
        {
            Client = client;
        }

        public TcpClient Client;
        public bool Connected = true;

        public int NetworkStreamOffset = 0;
    }
}