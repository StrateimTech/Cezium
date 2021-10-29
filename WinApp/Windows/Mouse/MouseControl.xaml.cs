using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WinApp.Windows.Mouse
{
    public partial class MouseControl : UserControl
    {
        public MouseControl()
        {
            InitializeComponent();
        }

        private void StateButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 1;
                MainGrid.Background = Brushes.Transparent;

                InvertMouseX.IsEnabled = true;
                InvertMouseY.IsEnabled = true;
                InvertMouseWheel.IsEnabled = true;
            });
        }

        private void StateButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 0.4;
                MainGrid.Background = Brushes.Black;

                InvertMouseX.IsEnabled = false;
                InvertMouseY.IsEnabled = false;
                InvertMouseWheel.IsEnabled = false;
            });
        }
    }
}