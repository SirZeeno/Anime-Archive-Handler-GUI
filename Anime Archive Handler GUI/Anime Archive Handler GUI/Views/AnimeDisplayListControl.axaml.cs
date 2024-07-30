using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Anime_Archive_Handler_GUI.Database_Handeling;
using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media.Imaging;
using DynamicData;

namespace Anime_Archive_Handler_GUI.Views;
using static Helpers.DailyFeatured;

public partial class AnimeDisplayListControl : UserControl
{
    private Grid _animeDynamicGrid;
    
    private static List<long?> _buffer = [];
    private static readonly Timer Timer = new(500);

    private AnimeDisplayListViewModel? animeListViewModel;
    private readonly Action<AnimeDto> _navigateToAnimeDetail;
    
    public AnimeDisplayListControl(Action<AnimeDto> navigateToAnime)
    {
        InitializeComponent();
        _navigateToAnimeDetail = navigateToAnime;
        animeListViewModel = DataContext as AnimeDisplayListViewModel;
        AnimeItemDisplayControl.AnimeListInstance = this;
        AnimeItemDisplayControl.SetGridItems();//.OnCompleted(AdjustGridLayout);
        AnimeTypeTabControl.SelectionChanged += AnimeTypeTabControl_SelectionChanged;
        AnimeDisplayListViewModel.AnimesToGetImagesFor.CollectionChanged += OnCollectionChanged;
        Timer.Elapsed += GetImagesForViewableAnimes;
        Timer.AutoReset = false; // Timer should not automatically reset, we will reset it manually
        Task.Run(InitializeAsync);
    }
    
    private async void ShowClickedAnime(object sender, RoutedEventArgs e)
    {
        string? animeName  = null;
        if (sender is not Button button) return;
        var templateChildren = button.GetLogicalChildren();
        foreach (var child in templateChildren)
        {
            if (child is not Border border) continue;
            var templateChildren2 = border.Child;
            if (templateChildren2 is not Grid grid) continue;
            var templateChildren3 = grid.Children;
            foreach (var gridChild in templateChildren3)
            {
                if (gridChild is not TextBlock textBlock) continue;
                animeName = textBlock.Text;
            }
        }

        if (animeName == null) return;
        var show = await Task.Run(() => SqlDbHandler.GetAnimeByTitle(animeName)); // This needs to contain the data from the database gathered by the name that the anime has displayed
        _navigateToAnimeDetail(show.First());
    }
    
    private async Task InitializeAsync()
    {
        var featuredItems = await PickDailyFeatured();
        await Task.Run(() =>
        {
            animeListViewModel.AnimeRecommendations ??= new AnimeCarousel(new ObservableCollection<CarouselItem>(featuredItems));
        });
        ConsoleExt.WriteLineWithPretext("Added Daily Featured", ConsoleExt.OutputType.Info);
    }
    
    private void AnimeTypeTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Get the selected tab item
        TabItem selectedTab = (TabItem)AnimeTypeTabControl.SelectedItem;

        switch (AnimeTypeTabControl.SelectedIndex)
        {
            // Set the content of the ContentControl based on the selected tab
            case 0:
                // Load content for Tab 1
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
    
    private void AnimeItemsControl_OnElementPrepared(object? sender, ItemsRepeaterElementPreparedEventArgs e)
    {
        if (e.Element is Button { DataContext: AnimeDisplayItem animeDisplayItem })
        {
            AnimeDisplayListViewModel.AnimesToGetImagesFor.Add(animeDisplayItem.AnimeId);
        }
    }

    private void AnimeItemsControl_OnElementClearing(object? sender, ItemsRepeaterElementClearingEventArgs e)
    {
        if (e.Element is Button { DataContext: AnimeDisplayItem animeDisplayItem })
        {
            AnimeDisplayListViewModel.AnimesToGetImagesFor.Remove(animeDisplayItem.AnimeId);
        }
    }
    
    private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                // Add new items to the buffer
                _buffer.AddRange(e.NewItems.OfType<long?>());
                break;
            case NotifyCollectionChangedAction.Remove:
                // Remove items from the buffer
                _buffer.Remove(e.OldItems.OfType<long?>().ToList());
                break;
            case NotifyCollectionChangedAction.Replace:
                break;
            case NotifyCollectionChangedAction.Move:
                break;
            case NotifyCollectionChangedAction.Reset:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // Restart the timer
        Timer.Stop();
        Timer.Start();
    }

    async void GetImagesForViewableAnimes(object? sender, ElapsedEventArgs elapsedEventArgs)
    {
        // Process the buffered items in bulk
        if (_buffer.Count <= 0) return;
        List<long?> itemsThatNeedImages = [.._buffer];
        _buffer.Clear();

        // Process itemsThatNeedImages as needed
        var result = await SqlDbHandler.GetAnimeBitmapImagesByIds(itemsThatNeedImages);
            
        foreach (var item in AnimeDisplayListViewModel.DynamicAnimeItemGrid)
        {
            if (!result.TryGetValue(item.AnimeId, out var imagesSet)) continue;
            if (imagesSet.JPG.ImageBitmap != null)
            {
                item.AnimeImage ??= new Bitmap(new MemoryStream(imagesSet.JPG.ImageBitmap));
            }
        }
    }
}