using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Anime_Archive_Handler_GUI.Converters;

public class SizeToPercentageSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double size)
        {
            return size / 100 * 20; // The last number is the percentage of the total size of the control it's allowed to take up
        }
        return 12; // Default size
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}