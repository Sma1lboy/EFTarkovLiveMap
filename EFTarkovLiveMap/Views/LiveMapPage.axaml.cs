using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EFTarkovLiveMap.ViewModels;

namespace EFTarkovLiveMap.Views;

public partial class LiveMapPage : UserControl
{
    private bool IsRecordingNextKey = true;
    private int MapWindowCount = 0;
    public LiveMapPage()
    {
        InitializeComponent();
        this.DataContext = new MainWindowViewModel();
        var vm = DataContext as MainWindowViewModel;
        vm.ShortCutKey = Key.F12;

    }

    private void RecordingButtonClick(object? sender, RoutedEventArgs e)
    {
        IsRecordingNextKey = true;
        RecordingText.Text = "Waiting for key press...";
        
    }

    private void KeyDownHandler(object? sender, KeyEventArgs e)
    {
        var vm = DataContext as MainWindowViewModel;
        if (IsRecordingNextKey)
        {
            // DataContext.ShortCutKey = e.Key;
            IsRecordingNextKey = false;
            vm.ShortCutKey = e.Key;
            RecordingText.Text = "Key set to " + e.Key;
        } else if (e.Key == vm.ShortCutKey )
        {
            
            vm.TurnOnOverlay = !vm.TurnOnOverlay;
            if (vm.TurnOnOverlay)
            {
                Map.GetInstance().Show();
            }
            else
            {
                Map.GetInstance().Hide();
            }
            
        }
    }

}