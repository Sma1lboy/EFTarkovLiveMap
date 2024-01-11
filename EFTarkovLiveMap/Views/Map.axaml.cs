using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace EFTarkovLiveMap.Views;

public partial class Map : Window
{
    private static Map _instance;

    public Map()
    {
        InitializeComponent();
    }
    private void OnClosing(object sender, CancelEventArgs e)
    {
        e.Cancel = true;  
        this.Hide();      
    }
    public static Map GetInstance()
    {
        return _instance ?? (_instance = new Map());
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void MouseMoving(object? sender, PointerEventArgs e)
    {
        Console.Out.WriteLine(e.GetPosition(this).X + " " + e.GetPosition(this).Y);
    }
}