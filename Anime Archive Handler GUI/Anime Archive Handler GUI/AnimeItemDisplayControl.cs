using System.Collections.ObjectModel;
using Anime_Archive_Handler_GUI.ViewModels;
using Anime_Archive_Handler_GUI.Views;
using Avalonia.Controls;
using Avalonia.Threading;
using DynamicData;

namespace Anime_Archive_Handler_GUI;

public class AnimeItemDisplayControl
{
    public static MainView MainViewInstance { get; set; } = null!;
    public static Grid AnimeItemGridInstance { get; set; } = null!;

    public static void SetGridItems(int count = 20)
    {
        ObservableCollection<AnimeDisplayItem> animeItems = new ObservableCollection<AnimeDisplayItem>();
        
        for (int i = 0; i < count; i++)
        {
            const int paddingThickness = 10;
            const int imageMaxWidth = 225;
            const int imageMaxHeight = 335;
            const int totalImageWidth = imageMaxWidth + paddingThickness * 2;
            var columnDefinition = new ColumnDefinition { Width = new GridLength(totalImageWidth, GridUnitType.Pixel) };
            //AnimeItemGridInstance.ColumnDefinitions.Add(columnDefinition);
            
            animeItems.Add(new AnimeDisplayItem("https://cdn.myanimelist.net/images/anime/4/19644l.jpg", "Cowboy Bebop", 12, 12, Language.Dub));
            animeItems.Add(new AnimeDisplayItem("https://cdn.myanimelist.net/images/anime/7/20310l.jpg", "Trigun", 12, 12, Language.Dub));
        }
        
        Dispatcher.UIThread.InvokeAsync(() => MainViewModel.DynamicAnimeItemGrid.AddRange(animeItems));
        //MainViewInstance.AddColumnToAnimeList("https://cdn.myanimelist.net/images/anime/4/19644l.jpg", "Cowboy Bebop", 12, 12, Language.Dub, 20);
        //MainViewInstance.AddColumnToAnimeList("https://cdn.myanimelist.net/images/anime/7/20310l.jpg", "Trigun", 12, 12, Language.Dub, 20);
    }
}