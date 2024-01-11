using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using EFTarkovLiveMap.ViewModels;

namespace EFTarkovLiveMap.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ContentArea.Content = new LiveMapPage();
        this.DataContext = new MainWindowViewModel();
    }

    public static bool IsRecordingNextKey = true;
    private void LiveMapClick(object? sender, RoutedEventArgs e)
    {
        ContentArea.Content = new LiveMapPage();
        
    }


}