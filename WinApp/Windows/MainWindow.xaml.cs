﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using WinApp.Windows.Home;
using WinApp.Windows.Keyboard;
using WinApp.Windows.Mouse;
using WinApp.Windows.Rust;

namespace WinApp.Windows
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private readonly UserControl _homeControl = new HomeControl();
        private readonly UserControl _mouseControl = new MouseControl();
        private readonly UserControl _keyboardControl = new KeyboardControl();
        private readonly UserControl _rustControl = new RustControl();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SidebarWidth = 75;
            TopContentHeight = 24;
            
            MainControl.Content = _homeControl;
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
            MinWidth = MainControl.Width;
            MinHeight = MainControl.Height;
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

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainControl.Content is not HomeControl)
            {
                MainControl.Content = _homeControl;
            }
        }

        private void MouseButtonPanel_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainControl.Content is not MouseControl)
            {
                MainControl.Content = _mouseControl;
            }
        }

        private void KeyboardButtonPanel_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainControl.Content is not KeyboardControl)
            {
                MainControl.Content = _keyboardControl;
            }
        }

        private void RustButtonPanel_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainControl.Content is not RustControl)
            {
                MainControl.Content = _rustControl;
            }
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
        }
    }
}