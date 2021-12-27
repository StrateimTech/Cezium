using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Server.Network.Packets;
using Server.Utils;

namespace Server.Network
{
    public class NetworkHandler
    {
        private TcpListener _serverListener;

        private bool _disposed;

        private readonly List<Tuple<TcpClient, ClientWrapper>> _clients = new();

        public void Start(int port)
        {
            if (!NetUtils.IsPortAvailable(port))
            {
                ConsoleUtils.WriteLine($"Couldn't bind to port {port}", GetType().Name);
                throw new Exception("Couldn't bind to a port.");
            }

            ConsoleUtils.WriteLine($"Binding TCPListener to port {port}", GetType().Name);
            (_serverListener = new TcpListener(IPAddress.Any, port)).Start();
            ConsoleUtils.WriteLine("Network server started, waiting for clients to connect!", GetType().Name);
            while (_serverListener.Server.IsBound && !_disposed)
            {
                try
                {
                    if (!_serverListener.Pending())
                    {
                        Thread.Sleep(1);
                        continue;
                    }

                    TcpClient client = _serverListener.AcceptTcpClient();
                    ConsoleUtils.WriteLine($"Accepted new connection{(Program.Settings.HideIp ? "" : $" (IP: {client.Client.RemoteEndPoint})")}",
                        GetType().Name);
                    new Task(() => { HandleClient(client); }).Start();
                }
                catch (Exception ex)
                {
                    ConsoleUtils.WriteLine($"{ex}", GetType().Name);
                }
            }
        }

        public void Stop()
        {
            _disposed = true;
            if (_serverListener != null)
            {
                ConsoleUtils.WriteLine("Disposing connected client connections", GetType().Name);
                _clients.ForEach(client =>
                {
                    client.Item2.Connected = false;
                    client.Item1.Close();
                });
                ConsoleUtils.WriteLine("Shutting down network server!", GetType().Name);
                _serverListener.Stop();
            }
        }

        private void HandleClient(TcpClient client)
        {
            var clientWrapper = new ClientWrapper(client);
            PacketHandler packetHandler = new PacketHandler(clientWrapper);
            var clientStream = client.GetStream();
            var clientTuple = new Tuple<TcpClient, ClientWrapper>(client, clientWrapper);
            
            _clients.Add(clientTuple);
            
            while (clientWrapper.Connected)
            {
                try
                {
                    if (!packetHandler.Handle(clientWrapper, clientStream))
                    {
                        clientWrapper.Connected = false;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleUtils.WriteLine(ex is IOException ? ex.Message : ex.ToString(), GetType().Name);
                    if (ex is IOException)
                    {
                        clientWrapper.Connected = false;
                    }
                    break;
                }
            }
            
            ConsoleUtils.WriteLine($"Client disconnected{(Program.Settings.HideIp ? "" : $" (IP: {client.Client.RemoteEndPoint})")}", GetType().Name);

            _clients.Remove(clientTuple);
            client.Dispose();
        }
    }
}