﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static WinApp.Utils.ServerUtil;

namespace WinApp.Windows.Rust.CodeLock
{
    public partial class RustLockControl : UserControl
    {
        public RustLockControl()
        {
            InitializeComponent();
        }

        private void StateButton_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateState(true);
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 1;
                MainGrid.Background = Brushes.Transparent;
            });
        }

        private void StateButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateState(false);
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 0.4;
                MainGrid.Background = Brushes.Black;
            });
        }

        private void DebugButton_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateDebugState(true);
        }

        private void DebugButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateDebugState(false);
        }
        
        private void UpdateState(bool state)
        {
            SendMessage($"4 KeyboardState {state}");
        }
        
        private void UpdateDebugState(bool state)
        {
            SendMessage($"4 DebugState {state}");
        }
    }
}