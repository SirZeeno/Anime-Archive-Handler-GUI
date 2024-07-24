using System.Collections.ObjectModel;
using Anime_Archive_Handler_GUI.Views;
using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class AnimeDisplayListViewModel : ViewModelBase
{
    public static AnimeDisplayListControl AnimeListInstance { get; set; }
    
    private AnimeCarousel? _animeRecommendations;
    
    // The carousel that displays the recommendations
    public AnimeCarousel? AnimeRecommendations
    {
        get => _animeRecommendations;
        set => this.RaiseAndSetIfChanged(ref _animeRecommendations, value);
    }
    
    // The list of anime that will be displayed
    public static ObservableCollection<AnimeDisplayItem> DynamicAnimeItemGrid { get; set; } = [];
    
    // Images that have to get loaded when you scroll through the list
    public static ObservableCollection<long?> AnimesToGetImagesFor { get; } = [];
}

public class YourResultType
{
    
}