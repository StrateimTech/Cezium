using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using HandyControl.Controls;
using HandyControl.Data;

namespace WinApp.Windows.Rust
{
    public partial class RustControl : UserControl
    {
        public RustControl()
        {
            InitializeComponent();
        }
        
        private void StateButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 1;
                MainGrid.Background = Brushes.Transparent;
                FovSlider.IsEnabled = true;
                SensitivitySlider.IsEnabled = true;
                SmoothnessSlider.IsEnabled = true;
                GunCombo.IsEnabled = true;
                ScopeCombo.IsEnabled = true;
                AttachmentCombo.IsEnabled = true;
                RandomizationToggle.IsEnabled = true;

                if (RandomizationToggle.IsChecked!.Value)
                {
                    ReverseRandomizationToggle.IsEnabled = true;
                    RandomizationSlider.IsEnabled = true;
                }
            });
        }

        private void StateButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGrid.Opacity = 0.4;
                MainGrid.Background = Brushes.Black;
                FovSlider.IsEnabled = false;
                SensitivitySlider.IsEnabled = false;
                SmoothnessSlider.IsEnabled = false;
                GunCombo.IsEnabled = false;
                ScopeCombo.IsEnabled = false;
                AttachmentCombo.IsEnabled = false;
                RandomizationToggle.IsEnabled = false;
                
                if (RandomizationToggle.IsChecked!.Value)
                {
                    ReverseRandomizationToggle.IsEnabled = false;
                    RandomizationSlider.IsEnabled = false;
                }
            });
        }
        
        private void FovSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FovLabel.Content = $"FOV: {(int)FovSlider.Value}";
        }

        private void FovSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            //TODO: API Here
        }

        private void SensitivitySlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var roundedSensitivity = Math.Round(SensitivitySlider.Value, 2);
            SensitivityLabel.Content = $"Sensitivity: {roundedSensitivity}";
        }

        private void SensitivitySlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            //TODO: API Here
        }

        private void SmoothnessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SmoothnessLabel.Content = $"Smoothness: {(int)SmoothnessSlider.Value}";
        }

        private void SmoothnessSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            //TODO: API Here
        }

        private void GunCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: API Here
        }

        private void AttachmentCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: API Here
        }

        private void ScopeCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: API Here
        }

        private void RandomizationToggle_OnUnchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ReverseRandomizationToggle.IsEnabled = false;
                ReverseRandomizationLabel.IsEnabled = false;
                
                RandomizationSliderLabel.IsEnabled = false;
                RandomizationSlider.IsEnabled = false;
            });
        }

        private void RandomizationToggle_OnChecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ReverseRandomizationToggle.IsEnabled = true;
                ReverseRandomizationLabel.IsEnabled = true;

                RandomizationSliderLabel.IsEnabled = true;
                RandomizationSlider.IsEnabled = true;
            });
        }

        private void RandomizationSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<DoubleRange> e)
        {
            RandomizationSliderLabel.Content = $"Randomization: {(int)e.NewValue.Start} - {(int)e.NewValue.End}";
        }
    }
}