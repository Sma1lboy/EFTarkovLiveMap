using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;

namespace EFTarkovLiveMap.Views;

public partial class Map : Window
{
    private static Map _instance;

    public Map()
    {
        InitializeComponent();
        InitializeWindowPosition();
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
    public void InitializeWindowPosition()
    {
        this.Position = new PixelPoint(0, 0);
        this.MinWidth = this.Width;
        this.MaxWidth = this.Width;
        this.MinHeight = this.Height;
        this.MaxHeight = this.Height;
        this.CanResize = false;
    }

    public void sourceImage(Bitmap source)
    {
        this.FindControl<Image>("myImageControll").Source = source;
    }
    private void MouseMoving(object? sender, PointerEventArgs e)
    {
        Console.Out.WriteLine(e.GetPosition(this).X + " " + e.GetPosition(this).Y);
    }
}