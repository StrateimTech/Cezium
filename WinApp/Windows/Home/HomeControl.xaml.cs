using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using WinApp.Utils;
using Timer = System.Timers.Timer;

namespace WinApp.Windows.Home
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            new Thread(() =>
            {
                var timer = new Timer();
                timer.Interval = 1000;
                timer.Elapsed += TimerEventProcessor;
                timer.Start();
            }).Start();
        }
        
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            var getUptime= ServerUtil.SendMessageWithReturn("3 GetUptime");
            var getMouseState = ServerUtil.SendMessageWithReturn("3 GetMouseState");
            var getKeyboardState = ServerUtil.SendMessageWithReturn("3 GetKeyboardState");
            var getRustState = ServerUtil.SendMessageWithReturn("3 GetRustState");
            var getServerVersion = ServerUtil.SendMessageWithReturn("3 GetServerVersion");
            Application.Current.Dispatcher.Invoke(() =>
            {
                UptimeContent.Content = getUptime;
                MouseContent.Content = getMouseState;
                KeyboardContent.Content = getKeyboardState;
                RustContent.Content = getRustState;
                ServerContent.Content = getServerVersion;
            });
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