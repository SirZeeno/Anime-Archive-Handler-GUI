using System;
using System.ComponentModel;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class SharedViewModel : INotifyPropertyChanged
{
    private int _selectedTabIndex;

    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set
        {
            if (_selectedTabIndex == value) return;
            _selectedTabIndex = value;
            OnPropertyChanged(nameof(SelectedTabIndex));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class TabIndexToDataTemplateConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int index = (int)value;
        ConsoleExt.WriteLineWithPretext(index, ConsoleExt.OutputType.Info);
        
        // Select data template based on tab index
        switch (index)
        {
            case 0:
                ConsoleExt.WriteLineWithPretext("Layout 0", ConsoleExt.OutputType.Info);
                return Application.Current.FindResource("HomePage");
            case 1:
                return Application.Current.FindResource("Layout2Template");
            // Add more cases for additional layouts
            default:
                return null;
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}