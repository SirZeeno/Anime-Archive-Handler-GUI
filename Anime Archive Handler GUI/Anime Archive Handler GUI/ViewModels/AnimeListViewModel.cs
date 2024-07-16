using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class AnimeListViewModel : ViewModelBase
{
    private AnimeCarousel? _animeRecommendations;
    public AnimeCarousel? AnimeRecommendations
    {
        get => _animeRecommendations;
        set => this.RaiseAndSetIfChanged(ref _animeRecommendations, value);
    }
    public static ObservableCollection<AnimeDisplayItem> DynamicAnimeItemGrid { get; set; } = [];
    
    public ObservableCollection<YourResultType> SearchResults { get; } = [];
    
    public static ObservableCollection<long?> AnimesToGetImagesFor { get; } = [];
    public bool IsPopupOpen { get; set; }
    public static int SelectedIndex { get; set; }
    
    private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
    {
        // Close the popup when the TextBox loses focus
        // I might want to add additional logic to check if the new focus target is the Popup itself
        IsPopupOpen = false;
    }
    
    private int _selectedAnimeHeaderMenuTabIndex;
    private int _selectedAnimeTypeMenuTabIndex;

    public int SelectedAnimeHeaderMenuTabIndex
    {
        get => _selectedAnimeHeaderMenuTabIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedAnimeHeaderMenuTabIndex, value);
    }
    
    public int SelectedAnimeTypeMenuTabIndex
    {
        get => _selectedAnimeTypeMenuTabIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedAnimeTypeMenuTabIndex, value);
    }
}

public class YourResultType
{
    
}