using System.Net.Sockets;

namespace Main.SettingsHandlers
{
    public abstract class SettingsInterface
    {
        public abstract void HandleData(byte[] data, TcpClient client);
        
        public abstract byte[]? HandleDataWithReturn(byte[] data, TcpClient client);

        public abstract void Start();
    }
}