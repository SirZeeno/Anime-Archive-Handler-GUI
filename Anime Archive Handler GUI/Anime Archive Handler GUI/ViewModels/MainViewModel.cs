using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class MainViewModel
{
    public static ObservableCollection<CarouselItem>? AnimePreviewItems { get; set; }
    
    public ObservableCollection<YourResultType> SearchResults { get; } = new ObservableCollection<YourResultType>();
    public bool IsPopupOpen { get; set; }
    
    public Panel Carousel { get; set; }
    
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