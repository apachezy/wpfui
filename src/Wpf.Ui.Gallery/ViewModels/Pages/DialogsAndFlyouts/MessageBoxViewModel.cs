// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Wpf.Ui.Gallery.ViewModels.Pages.DialogsAndFlyouts;

public partial class MessageBoxViewModel : ViewModel
{
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "relay command")]
    [RelayCommand]
    private void OnOpenStandardMessageBox(object sender)
    {
        _ = MessageBox.Show("Something about to happen", "I can feel it");
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "relay command")]
    [RelayCommand]
    private async Task OnOpenCustomMessageBox(object sender)
    {
        var uiMessageBox = new Wpf.Ui.Controls.MessageBox
        {
            Title = "WPF UI Message Box",
        };

        var textBlock = new TextBlock
        {
            Text = "Never gonna give you up, never gonna let you down Never gonna run around and desert you Never gonna make you cry, never gonna say goodbye",
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 8),
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var button = new Button
        {
            Content = "Action",
            Height = 36,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Margin = new Thickness(0, 0, 0, 8)
        };

        var checkBox = new CheckBox
        {
            Content = "Increase button height",
            IsChecked = false
        };

        // Animate height on check/uncheck instead of setting it directly
        checkBox.Checked += (_, _) =>
        {
            var animation = new DoubleAnimation
            {
                To = 72,
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            button.BeginAnimation(FrameworkElement.HeightProperty, animation);
        };

        checkBox.Unchecked += (_, _) =>
        {
            var animation = new DoubleAnimation
            {
                To = 36,
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            button.BeginAnimation(FrameworkElement.HeightProperty, animation);
        };

        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Vertical
        };

        _ = stackPanel.Children.Add(textBlock);
        _ = stackPanel.Children.Add(button);
        _ = stackPanel.Children.Add(checkBox);

        uiMessageBox.Content = stackPanel;

        _ = await uiMessageBox.ShowDialogAsync();
    }
}
