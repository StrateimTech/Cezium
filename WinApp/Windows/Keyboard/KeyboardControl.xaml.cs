using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WinApp.Windows.Keyboard
{
    public partial class KeyboardControl : UserControl
    {
        public KeyboardControl()
        {
            InitializeComponent();
        }

        private void StateButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 1;
                MainGrid.Background = Brushes.Transparent;
            });
        }

        private void StateButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 0.4;
                MainGrid.Background = Brushes.Black;
            });
        }
    }
}