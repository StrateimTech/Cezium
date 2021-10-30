using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using HandyControl.Data;
using WinApp.Windows.Home;
using static WinApp.Utils.ServerUtil;

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
            UpdateState(true);
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
            UpdateState(false);
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
            UpdateFov((int)FovSlider.Value);
        }

        private void SensitivitySlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SensitivityLabel.Content = $"Sensitivity: {Math.Round(SensitivitySlider.Value, 2)}";
        }

        private void SensitivitySlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            UpdateSens(Math.Round(SensitivitySlider.Value, 2));
        }

        private void SmoothnessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SmoothnessLabel.Content = $"Smoothness: {(int)SmoothnessSlider.Value}";
        }

        private void SmoothnessSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            UpdateSmooth((int)SmoothnessSlider.Value);
        }

        private void GunCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GunCombo is not null && ScopeCombo is not null && AttachmentCombo is not null)
            {
                var gunItem = (ComboBoxItem)GunCombo.SelectedItem;
                var scopeItem = (ComboBoxItem)ScopeCombo.SelectedItem;
                var attachmentItem = (ComboBoxItem)AttachmentCombo.SelectedItem;
            
                string? gunValue = gunItem.Content.ToString();
                string? scopeValue = scopeItem.Content.ToString();
                string? attachmentValue = attachmentItem.Content.ToString();
                if (scopeValue is not null && gunValue is not null && attachmentValue is not null)
                {
                    switch (scopeValue.ToLower())
                    {
                        case "8x scope":
                            scopeValue = "Zoom8Scope";
                            break;
                        case "16x scope":
                            scopeValue = "Zoom16Scope";
                            break;
                        case "holosight":
                            scopeValue = "HoloSight";
                            break;
                        case "handmade sight":
                            scopeValue = "HandmadeSight";
                            break;
                    }
                    UpdateGun(gunValue.Replace(" ", ""), scopeValue.Replace(" ", ""), attachmentValue.Replace(" ", ""));
                } 
            }
        }

        private void AttachmentCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GunCombo is not null && ScopeCombo is not null && AttachmentCombo is not null)
            {
                var gunItem = (ComboBoxItem)GunCombo.SelectedItem;
                var scopeItem = (ComboBoxItem)ScopeCombo.SelectedItem;
                var attachmentItem = (ComboBoxItem)AttachmentCombo.SelectedItem;
            
                string? gunValue = gunItem.Content.ToString();
                string? scopeValue = scopeItem.Content.ToString();
                string? attachmentValue = attachmentItem.Content.ToString();
                if (scopeValue is not null && gunValue is not null && attachmentValue is not null)
                {
                    switch (scopeValue.ToLower())
                    {
                        case "8x scope":
                            scopeValue = "Zoom8Scope";
                            break;
                        case "16x scope":
                            scopeValue = "Zoom16Scope";
                            break;
                        case "holosight":
                            scopeValue = "HoloSight";
                            break;
                        case "handmade sight":
                            scopeValue = "HandmadeSight";
                            break;
                    }
                    UpdateGun(gunValue.Replace(" ", ""), scopeValue.Replace(" ", ""), attachmentValue.Replace(" ", ""));
                } 
            }
        }

        private void ScopeCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GunCombo is not null && ScopeCombo is not null && AttachmentCombo is not null)
            {
                var gunItem = (ComboBoxItem)GunCombo.SelectedItem;
                var scopeItem = (ComboBoxItem)ScopeCombo.SelectedItem;
                var attachmentItem = (ComboBoxItem)AttachmentCombo.SelectedItem;
            
                string? gunValue = gunItem.Content.ToString();
                string? scopeValue = scopeItem.Content.ToString();
                string? attachmentValue = attachmentItem.Content.ToString();
                if (scopeValue is not null && gunValue is not null && attachmentValue is not null)
                {
                    switch (scopeValue.ToLower())
                    {
                        case "8x scope":
                            scopeValue = "Zoom8Scope";
                            break;
                        case "16x scope":
                            scopeValue = "Zoom16Scope";
                            break;
                        case "holosight":
                            scopeValue = "HoloSight";
                            break;
                        case "handmade sight":
                            scopeValue = "HandmadeSight";
                            break;
                    }
                    UpdateGun(gunValue.Replace(" ", ""), scopeValue.Replace(" ", ""), attachmentValue.Replace(" ", ""));
                } 
            }
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

        private void UpdateGun(string gun, string scope, string attachment)
        {
            SendMessage($"2 ChangeGun {gun} {scope} {attachment}");
        }

        private void UpdateSens(double sens)
        {
            SendMessage($"2 ChangeSens {sens}");
        }

        private void UpdateFov(int fov)
        {
            SendMessage($"2 ChangeFov {fov}");
        }

        private void UpdateSmooth(int smooth)
        {
            SendMessage($"2 ChangeSmoothness {smooth}");
        }

        private void UpdateState(bool state)
        {
            SendMessage($"2 ChangeState {state}");
        }
    }
}