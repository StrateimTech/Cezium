using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

namespace WinApp.Windows.Home
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
        }

        public static string? ServerIp;
        public static int? ServerPort;

        private void ServerIPTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ServerIPTextBox.Text.Length > 0 && ServerIPTextBox.Text.Contains(":"))
            {
                //ip:port
                var splitData = ServerIPTextBox.Text.Split(":");
                if (splitData.Length > 0 && splitData[1].Length > 0)
                {
                    ServerIp = splitData[0];
                    int.TryParse(splitData[1], out int serverPort);
                    ServerPort = serverPort;
                    Trace.WriteLine($"{ServerIp} {ServerPort}");
                }
            }
        }
    }
}