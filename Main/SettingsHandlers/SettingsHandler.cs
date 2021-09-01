using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Main.HID;
using Main.SettingsHandlers.Rust;

namespace Main.SettingsHandlers
{
    public class SettingsHandler
    {
        private readonly Settings _settings;
        private readonly HidHandler _hidHandler;
        private SettingsInterface[] _settingsHandlers;

        public SettingsHandler(Settings settings, HidHandler hidHandler)
        {
            _settings = settings;
            _settingsHandlers = new SettingsInterface[] { };
            _hidHandler = hidHandler;
            var settingsThreadHandler = new Thread(() =>
            {
                _settingsHandlers = new SettingsInterface[] { new RustHandler(settings, hidHandler) };
                foreach (var settingsInterface in _settingsHandlers)
                {
                    settingsInterface.Start();
                }
            })
            {
                            IsBackground = true
            };
            settingsThreadHandler.Start();
        }
        
        

        public void HandleData(byte[] data, TcpClient client)
        {
            var stringData = Encoding.ASCII.GetString(data);
            var stringSplit = stringData.Split("_");
            if (stringSplit[0].Equals("G", StringComparison.OrdinalIgnoreCase))
            {
                switch (stringSplit[2].ToUpper())
                {
                    case "INVERTMOUSEY":
                        Boolean.TryParse(stringSplit[3], out bool mouseYBool);
                        _settings.General.InvertMouseY = mouseYBool;
                        break;
                    case "INVERTMOUSEX":
                        Boolean.TryParse(stringSplit[3], out bool mouseXBool);
                        _settings.General.InvertMouseX = mouseXBool;
                        break;
                    case "CURRENTGAME":
                        Enum.TryParse(stringSplit[3], out Settings.GeneralSettings.Game currentGame);
                        _settings.General.CurrentGame = currentGame;
                        break;
                }
            }
            else
            {
                switch (_settings.General.CurrentGame)
                {
                    case Settings.GeneralSettings.Game.Rust:
                        _settingsHandlers[0].HandleData(data, client);
                        break;
                }
            }
        }
        
        public byte[]? HandleDataWithReturn(byte[] data, TcpClient client)
        {
            var stringData = Encoding.ASCII.GetString(data);
            var stringSplit = stringData.Split("_");
            if (stringSplit[0].Equals("G", StringComparison.OrdinalIgnoreCase))
            {
                switch (stringSplit[2].ToUpper())
                {
                    case "INVERTMOUSEY":
                        return BitConverter.GetBytes(_settings.General.InvertMouseY);
                        break;
                    case "INVERTMOUSEX":
                        return BitConverter.GetBytes(_settings.General.InvertMouseX);
                        break;
                    case "CURRENTGAME":
                        if (_settings.General.CurrentGame != null)
                            return Encoding.ASCII.GetBytes(Enum.GetName(typeof(Settings.GeneralSettings.Game), _settings.General?.CurrentGame));
                        else
                            return Encoding.ASCII.GetBytes("None");
                        break;
                }
            }
            else
            {
                switch (_settings.General.CurrentGame)
                {
                    case Settings.GeneralSettings.Game.Rust:
                        return _settingsHandlers[0].HandleDataWithReturn(data, client);
                }
            }
            return null;
        }
    }
}