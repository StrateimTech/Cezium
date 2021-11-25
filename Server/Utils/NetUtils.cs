using System.Linq;
using System.Net.NetworkInformation;

namespace Server.Utils
{
    public static class NetUtils
    {
        public static bool IsPortAvailable(int port)
        {
            return IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners()
                .All(ipEndPoint => !ipEndPoint.Port.Equals(port));
        }
    }
}