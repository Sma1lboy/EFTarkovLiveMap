using System;
using System.Drawing;
using System.Drawing.Imaging;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using EFTarkovLiveMap.ViewModels;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

using System.IO;
using EFTarkovLiveMap.Utils;
using Microsoft.CodeAnalysis.Text;

namespace EFTarkovLiveMap.Views;

public partial class LiveMapPage : UserControl
{
    private bool IsRecordingNextKey = true;
    private int MapWindowCount = 0;
    private string imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Escape from Tarkov", "Screenshots");
    public LiveMapPage()
    {
        InitializeComponent();
        this.DataContext = new MainWindowViewModel();
        var vm = DataContext as MainWindowViewModel;
        vm.ShortCutKey = Key.F9;

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
            IsRecordingNextKey = false;
            vm.ShortCutKey = e.Key;
            RecordingText.Text = "Key set to " + e.Key;
        } else if (e.Key == vm.ShortCutKey )
        {
            
            vm.TurnOnOverlay = !vm.TurnOnOverlay;
            if (vm.TurnOnOverlay)
            {
                var a = ComboBox.SelectionBoxItem.ToString();
                var web = Map.GetInstance();
                Avalonia.Media.Imaging.Bitmap map = 
                    MapImageFetcher.FetchMapImage(ComboBox.SelectionBoxItem.ToString());
                if (map == null)
                {
                    Console.Write("the name of the map is not correct");
                    return;
                }
                web.sourceImage(map);
                web.Show();
            }
            else
            {
                Map.GetInstance().Hide();
            }
        }
    }

}