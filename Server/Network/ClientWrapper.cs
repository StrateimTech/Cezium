using System.Security.Cryptography;

namespace Server.Network
{
    public class ClientWrapper
    {
        public ClientWrapper()
        {
        }

        public bool Encryption = false;
        
        public bool Connected = true;
    }
}