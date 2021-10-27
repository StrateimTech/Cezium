using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SidebarWidth = 75;
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