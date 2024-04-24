using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class MainViewModel
{
    public static ObservableCollection<CarouselItem>? AnimePreviewItems { get; set; }
    public static ObservableCollection<AnimeDisplayItem> DynamicAnimeItemGrid { get; set; } = [];
    
    public ObservableCollection<YourResultType> SearchResults { get; } = [];
    public bool IsPopupOpen { get; set; }

    
    public static int SelectedIndex { get; set; }
    
    private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
    {
        // Close the popup when the TextBox loses focus
        // I might want to add additional logic to check if the new focus target is the Popup itself
        IsPopupOpen = false;
    }
}

public class YourResultType
{
}