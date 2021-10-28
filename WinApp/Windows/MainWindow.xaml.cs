using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using WinApp.Windows.Home;

namespace WinApp.Windows
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SidebarWidth = 75;
            TopContentHeight = 24;
            
            MainControl.Content = new HomeControl();
            MainControl.Width = Width - SidebarWidth;
            MainControl.Height = Height - TopContentHeight;
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        private void WindowDrag(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, 0xA1, (IntPtr)0x2, (IntPtr)0);
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TopContentWidth = e.NewSize.Width - SidebarWidth - 2;
            
            MainControl.Width = e.NewSize.Width - SidebarWidth;
            MainControl.Height = e.NewSize.Height - TopContentHeight;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private double _topContentWidth;

        public double TopContentWidth
        {  
            get => _topContentWidth;
            set
            {
                _topContentWidth = value;
                NotifyPropertyChanged(Name);
            }
        }
        
        private double _topContentHeight;

        public double TopContentHeight
        {  
            get => _topContentHeight;
            set
            {
                _topContentHeight = value;
                NotifyPropertyChanged(Name);
            }
        }
        
        
        private int _sidebarWidth;

        public int SidebarWidth
        {  
            get => _sidebarWidth;
            set
            {
                _sidebarWidth = value;
                NotifyPropertyChanged(Name);
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}