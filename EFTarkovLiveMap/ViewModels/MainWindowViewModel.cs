using System;
using System.ComponentModel;
using System.Diagnostics;
using Avalonia.Input;

namespace EFTarkovLiveMap.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
      
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
    /// <summary>
    /// ShortCutKey binding for turn on/off map overlay
    /// </summary>
    private Key _shortCutKey = Key.F9;
    public Key ShortCutKey
    {
        get => _shortCutKey;
        set
        {
            _shortCutKey = value;
            OnPropertyChanged(nameof(ShortCutKey));
        }
    }
    private bool _turnOnOverlay = false;
    public bool TurnOnOverlay
    {
        get => _turnOnOverlay;
        set
        {
            _turnOnOverlay = value; 
            OnPropertyChanged(nameof(TurnOnOverlay));
        }
    }



}