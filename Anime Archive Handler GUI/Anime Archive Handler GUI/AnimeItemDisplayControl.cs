using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using DynamicData;

namespace Anime_Archive_Handler_GUI;
using Database_Handeling;
using ViewModels;
using Views;

public class AnimeItemDisplayControl
{
    public static AnimeDisplayListControl AnimeListInstance { get; set; } = null!;
    
    private const int PaddingThickness = 10;
    private const int ImageMaxWidth = 225;
    private const int ImageMaxHeight = 335;
    private const int TotalImageWidth = ImageMaxWidth + PaddingThickness * 2; // Responsible for the column spacing that each square of the grid takes
    private const int TotalImageHeight = ImageMaxHeight + PaddingThickness * 2; // Responsible for the row spacing that each square of the grid takes
    private const int AnimeListViewColor = 32;
    
    public static void SetGridItems(int count = 50000) //TODO: add a folder-watch so this can be updated when new animes are added
    {
        ObservableCollection<AnimeDisplayItem> animeItems = [];

        IEnumerable<AnimeDto> animeList = SqlDbHandler.GetAnimesByCount(count);
        List<long?> malIds = new List<long?>();
        
        foreach (var searchResult in animeList) // (Warning) if i convert this into linq this will make tons of db calls
        {
            malIds.Add(searchResult.MalId);
        }

        // TODO: use images that have been reduced in size by bitmaptowidth
        Dictionary<long?, ICollection<TitleEntryDto>> titles = SqlDbHandler.GetAnimeTitlesByIds(malIds);

        foreach (var malId in malIds)
        {
            titles.TryGetValue(malId, out var titleEntries);
            string title = (titleEntries.Where(x => x.Type.ToLower() == "english").Select(x => x.Title).FirstOrDefault() ?? titleEntries.Where(x => x.Type.ToLower() == "default").Select(x => x.Title).FirstOrDefault()) ?? string.Empty;
            
            animeItems.Add(new AnimeDisplayItem(malId, title, 12, 12, 12, Language.Dub));
        }
        
        ConsoleExt.WriteLineWithPretext("Picked Grid Items", ConsoleExt.OutputType.Info);
        Dispatcher.UIThread.InvokeAsync(() => AnimeDisplayListViewModel.DynamicAnimeItemGrid.AddRange(animeItems)).GetAwaiter();
    }
    
    public TaskAwaiter AddAnimeToAnimeGrid(AnimeDisplayItem animeItem, int count = 1)
    {
        ObservableCollection<AnimeDisplayItem> animeItems = [];
        
        for (int i = 0; i < count; i++)
        {
            animeItems.Add(animeItem);
        }
        
        ConsoleExt.WriteLineWithPretext("Added Anime Grid Items", ConsoleExt.OutputType.Info);
        return Dispatcher.UIThread.InvokeAsync(() => AnimeDisplayListViewModel.DynamicAnimeItemGrid.AddRange(animeItems)).GetAwaiter();
    }

    internal static async Task UserAddAnimeToAnimeGrid() //TODO: Add functionality for user to add anime to grid
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(AnimeListInstance);

        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Anime File(s)",
            AllowMultiple = true
        });
    }
    internal static async Task UserAddAnimeEpisodeToAnimeGrid() //TODO: Add functionality for user to add anime to grid
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(AnimeListInstance);

        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Open Anime Folder(s)",
            AllowMultiple = true
        });
    }
}