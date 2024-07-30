using System;
using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Anime_Archive_Handler_GUI.Converters;

public class ListHasItemsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable list)
        {
            return list.GetEnumerator().MoveNext();
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}