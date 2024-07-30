using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Anime_Archive_Handler_GUI.Converters;

public class ListSeparatorConverter : IMultiValueConverter
{
    /*
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var currentItem = parameter;
        if (value is IList items && currentItem != null)
        {
            return items.IndexOf(currentItem) != items.Count - 1;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    */

    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count >= 2)
        {
            var currentItem = values[0];
            var items = values[1] as IEnumerable<object>;

            if (items != null && currentItem != null)
            {
                var itemsList = items.ToList();
                return itemsList.IndexOf(currentItem) != itemsList.Count - 1;
            }
        }

        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}