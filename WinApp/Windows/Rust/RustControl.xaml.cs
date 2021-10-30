using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
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
            //TODO: API Here
        }

        private void SensitivitySlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var roundedSensitivity = Math.Round(SensitivitySlider.Value, 2);
            SensitivityLabel.Content = $"Sensitivity: {roundedSensitivity}";
        }

        private void SensitivitySlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            UpdateSens(SensitivitySlider.Value);
            //TODO: API Here
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
        
        private const string Server = "192.168.0.180";
        private const int Port = 200;

        private void UpdateGun(string gun, string scope, string attachment)
        {
            Connect(Server, Port, $"ChangeGun {gun} {scope} {attachment}");
        }

        private void UpdateSens(double sens)
        {
            Connect(Server, Port, $"ChangeSens {sens}");
        }

        private void UpdateFov(int fov)
        {
            Connect(Server, Port, $"ChangeFov {fov}");
        }

        private void UpdateSmooth(int smooth)
        {
            Connect(Server, Port, $"ChangeSmoothness {smooth}");
        }

        private void UpdateState(bool state)
        {
            Connect(Server, Port, $"ChangeState {state}");
        }

        private void Connect(string server, int port, string message)
        {
            (new Thread(() => {
                try
                {
                    TcpClient client = new TcpClient(server, port);
        
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        
                    NetworkStream stream = client.GetStream();
        
                    stream.Write(data, 0, data.Length);
        
                    Console.WriteLine("Sent: {0}", message);
        
                    data = new byte[256];
        
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    string responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);
        
                    stream.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }
            })).Start();
        }
    }
}