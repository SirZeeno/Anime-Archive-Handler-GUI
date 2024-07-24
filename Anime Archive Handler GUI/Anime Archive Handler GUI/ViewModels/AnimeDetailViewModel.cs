using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class AnimeDetailViewModel : ViewModelBase
{
    private AnimeDto _animeToDisplay;
    public AnimeDto AnimeToDisplay
    {
        get => _animeToDisplay;
        set => this.RaiseAndSetIfChanged(ref _animeToDisplay, value);
    }
}