using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using WinApp.Windows.Home;

namespace WinApp.Utils
{
    public static class ServerUtil
    {
        public static void SendMessage(string message)
        {
            new Thread(() => {
                if (HomeControl.ServerIp is not null && HomeControl.ServerPort is not null)
                {
                    try
                    {
                        string server = HomeControl.ServerIp;
                        int port = HomeControl.ServerPort.Value;
                        var client = new TcpClient(server, port);
        
                        var data = System.Text.Encoding.ASCII.GetBytes(message);
        
                        var stream = client.GetStream();
        
                        stream.Write(data, 0, data.Length);
        
                        data = new byte[256];
        
                        var bytes = stream.Read(data, 0, data.Length);
                        System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        
                        stream.Close();
                        client.Close();
                    }
                    catch (Exception e)
                    {
                        if(e is not SocketException)
                            Trace.WriteLine($"Exception: {e}");
                    }
                }
            }).Start();
        }
        
        // Make sure to use thread
        public static string SendMessageWithReturn(string message)
        {
            if (HomeControl.ServerIp is not null && HomeControl.ServerPort is not null)
            {
                try
                {
                    string server = HomeControl.ServerIp;
                    int port = HomeControl.ServerPort.Value;
                    var client = new TcpClient(server, port);
        
                    var data = System.Text.Encoding.ASCII.GetBytes(message);
        
                    var stream = client.GetStream();
        
                    stream.Write(data, 0, data.Length);
        
                    data = new byte[256];
        
                    var bytes = stream.Read(data, 0, data.Length);
                    var fromServer =System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        
                    stream.Close();
                    client.Close();
                    return fromServer;
                }
                catch (Exception e)
                {
                    if(e is not SocketException)
                        Trace.WriteLine($"Exception: {e}");
                }
            }
            return "Unknown";
        }
    }
}