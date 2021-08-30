using System.Linq;
using System.Net.NetworkInformation;

namespace Main.Utils
{
    public class NetUtils
    {
        public static bool PortAvailable(int portId)
        {
            return IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections()
                            .All(tcpInfo => !tcpInfo.LocalEndPoint.Port.Equals(portId));
        }
    }
}