﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using HandyControl.Data;
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
                    RandomizationXSlider.IsEnabled = true;
                    RandomizationYSlider.IsEnabled = true;
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
                    RandomizationXSlider.IsEnabled = false;
                    RandomizationYSlider.IsEnabled = false;
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
            UpdateRandomization(false);
            Application.Current.Dispatcher.Invoke(() =>
            {
                ReverseRandomizationToggle.IsEnabled = false;
                ReverseRandomizationLabel.IsEnabled = false;
                
                RandomizationXSliderLabel.IsEnabled = false;
                RandomizationXSlider.IsEnabled = false;
                
                RandomizationYSliderLabel.IsEnabled = false;
                RandomizationYSlider.IsEnabled = false;
            });
        }

        private void RandomizationToggle_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateRandomization(true);
            Application.Current.Dispatcher.Invoke(() =>
            {
                ReverseRandomizationToggle.IsEnabled = true;
                ReverseRandomizationLabel.IsEnabled = true;
            
                RandomizationXSliderLabel.IsEnabled = true;
                RandomizationXSlider.IsEnabled = true;
                
                RandomizationYSliderLabel.IsEnabled = true;
                RandomizationYSlider.IsEnabled = true;
            });
        }
        
        private void RandomizationXSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<DoubleRange> e)
        {
            RandomizationXSliderLabel.Content = $"RandomizationX: {(int)e.NewValue.Start} - {(int)e.NewValue.End}";
            UpdateRandomizationAmountX((int) e.NewValue.Start, (int) e.NewValue.End);
        }
        
        private void RandomizationYSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<DoubleRange> e)
        {
            RandomizationYSliderLabel.Content = $"RandomizationY: {(int)e.NewValue.Start} - {(int)e.NewValue.End}";
            UpdateRandomizationAmountY((int) e.NewValue.Start, (int) e.NewValue.End);
        }
        
        private void HorizontalModifierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HorizontalModifierLabel.Content = $"Horizontal Modifier: {Math.Round(HorizontalModifierSlider.Value, 2)}";
            UpdateRecoilModifierX(Math.Round(HorizontalModifierSlider.Value, 2));
        }

        private void VerticalModifierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VerticalModifierLabel.Content = $"Vertical Modifier: {Math.Round(VerticalModifierSlider.Value, 2)}";
            UpdateRecoilModifierY(Math.Round(VerticalModifierSlider.Value, 2));
        }
        
        private void DebugButton_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateDebugState(true);
        }

        private void DebugButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateDebugState(false);
        }
        
        private void ReverseRandomizationToggle_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateReverseRandomization(false);
        }

        private void ReverseRandomizationToggle_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateReverseRandomization(true);
        }
        
        private void AmmoReset_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateAmmoReset(false);
        }

        private void AmmoReset_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateAmmoReset(true);
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

        private void UpdateAmmoReset(bool state)
        {
            SendMessage($"2 ChangeAmmoReset {state}");
        }
        
        private void UpdateRandomization(bool state)
        {
            SendMessage($"2 ChangeRandomization {state}");
        }
        
        private void UpdateReverseRandomization(bool state)
        {
            SendMessage($"2 ChangeReverseRandomization {state}");
        }
        
        private void UpdateRandomizationAmountX(int start, int end)
        {
            SendMessage($"2 ChangeRandomizationAmountX {start} {end}");
        }
        
        private void UpdateRandomizationAmountY(int start, int end)
        {
            SendMessage($"2 ChangeRandomizationAmountY {start} {end}");
        }
        
        private void UpdateRecoilModifierX(double value)
        {
            SendMessage($"2 ChangeRecoilModifierX {value}");
        }
        
        private void UpdateRecoilModifierY(double value)
        {
            SendMessage($"2 ChangeRecoilModifierY {value}");
        }
        
        private void UpdateDebugState(bool state)
        {
            SendMessage($"2 DebugState {state}");
        }
    }
}