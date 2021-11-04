using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static WinApp.Utils.ServerUtil;

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
            UpdateState(true);
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
            UpdateState(false);
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 0.4;
                MainGrid.Background = Brushes.Black;

                InvertMouseX.IsEnabled = false;
                InvertMouseY.IsEnabled = false;
                InvertMouseWheel.IsEnabled = false;
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

        private void InvertMouseX_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateInvertMouseX(false);
        }

        private void InvertMouseX_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateInvertMouseX(true);
        }

        private void InvertMouseY_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateInvertMouseY(false);
        }

        private void InvertMouseY_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateInvertMouseY(true);
        }

        private void InvertMouseWheel_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateInvertMouseWheel(false);
        }

        private void InvertMouseWheel_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateInvertMouseWheel(true);
        }
        
        private void UpdateState(bool state)
        {
            SendMessage($"0 MouseState {state}");
        }
        
        private void UpdateInvertMouseY(bool state)
        {
            SendMessage($"0 InvertMouseY {state}");
        }
        
        private void UpdateInvertMouseX(bool state)
        {
            SendMessage($"0 InvertMouseX {state}");
        }
        
        private void UpdateInvertMouseWheel(bool state)
        {
            SendMessage($"0 InvertMouseWheel {state}");
        }
        
        private void UpdateDebugState(bool state)
        {
            SendMessage($"0 DebugState {state}");
        }
    }
}