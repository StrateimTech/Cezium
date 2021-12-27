namespace Loader.Network
{
    public class ServerWrapper
    {
        public ServerWrapper()
        {
        }
        
        public bool Encryption = false;
        public bool Authed = true;

        public bool Connected = false;

        public bool? VersionSynced;
    }
}