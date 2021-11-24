namespace Server.Network
{
    public class ClientWrapper
    {
        public ClientWrapper()
        {
        }

        public int BuildNumber = 0;

        public bool Encryption = false;
        public bool Authed = false;
        
        public bool Connected = true;
    }
}