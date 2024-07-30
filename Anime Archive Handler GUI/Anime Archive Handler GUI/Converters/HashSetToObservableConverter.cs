using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace Anime_Archive_Handler_GUI.Converters;

public class HashSetToObservableConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable enumerable)
        {
            var elementType = value.GetType().GetGenericArguments().FirstOrDefault();
            var observableCollectionType = typeof(ObservableCollection<>).MakeGenericType(elementType);
            var observableCollection = Activator.CreateInstance(observableCollectionType, enumerable) as IList;
            return observableCollection;
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable enumerable)
        {
            var elementType = value.GetType().GetGenericArguments().FirstOrDefault();
            var hashSetType = typeof(HashSet<>).MakeGenericType(elementType);
            var hashSet = Activator.CreateInstance(hashSetType, enumerable) as IList;
            return hashSet;
        }
        return null;
    }
}