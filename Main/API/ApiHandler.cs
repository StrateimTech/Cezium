using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Main.SettingsHandlers;
using Main.Utils;

namespace Main.API
{
    public class ApiHandler
    {
        private readonly ushort _port;
        private bool _running;
        private readonly Settings _settings;
        private readonly SettingsHandler _settingsHandler;
        
        
        public ApiHandler(ushort port, Settings settings, SettingsHandler settingsHandler)
        {
            _port = port;
            _settings = settings;
            _settingsHandler = settingsHandler;
        }

        public void Start()
        {
            _running = true;
            var server = new TcpListener(IPAddress.Any, 200);
            server.Start();
            Console.WriteLine("TCP Listener started.");

            Byte[] bytes = new Byte[256];
            while (_running)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client has connected!");

                NetworkStream stream = client.GetStream();

                try
                {
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    { 
                        var stringData = Encoding.ASCII.GetString(bytes, 0, i);
                        var stringSplit = stringData.Split("_");
                        
                        //G_GET_INVERTMOUSEY_VALUE
                        
                        //R_GET_
                        //R_POST_
                        
                        //R_GET_FOV
                        switch (stringSplit[0].ToUpper())
                        {
                            case "G":
                                switch (stringSplit[1].ToUpper())
                                {
                                    case "GET":
                                        _settingsHandler.HandleDataWithReturn(bytes, client);
                                        break;
                                    case "POST":
                                        _settingsHandler.HandleData(bytes, client);
                                        break;
                                }
                                break;
                            case "R":
                                switch (stringSplit[1].ToUpper())
                                {
                                    case "GET":
                                        _settingsHandler.HandleDataWithReturn(bytes, client);
                                        break;
                                    case "POST":
                                        _settingsHandler.HandleData(bytes, client);
                                        break;
                                }
                                break;
                        }
                    }
                    client.Close();
                }
                catch (Exception ex)
                {
                    ConsoleUtils.WriteCentered($"Exception. {ex.Message}");
                }
                finally
                {
                    client.Close();
                }
            }
        }

        public void Stop()
        {
            _running = false;
        }
    }
}