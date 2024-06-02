using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Anime_Archive_Handler_GUI.Converters;

public class EnumToBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        return value.Equals(parameter);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null && value.Equals(true))
            return parameter;

        return BindingOperations.DoNothing;
    }
}