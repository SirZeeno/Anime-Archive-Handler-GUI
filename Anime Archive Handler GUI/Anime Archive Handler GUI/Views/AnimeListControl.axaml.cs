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
using Avalonia.Media.Imaging;
using DynamicData;

namespace Anime_Archive_Handler_GUI.Views;
using static Helpers.DailyFeatured;

public partial class AnimeListControl : UserControl
{
    private Grid _animeDynamicGrid;
    
    private static List<long?> _buffer = [];
    private static readonly Timer Timer = new(500);

    private AnimeListViewModel? animeListViewModel;
    
    public AnimeListControl()
    {
        InitializeComponent();
        animeListViewModel = DataContext as AnimeListViewModel;
        AnimeItemDisplayControl.AnimeListInstance = this;
        AnimeItemDisplayControl.SetGridItems();//.OnCompleted(AdjustGridLayout);
        ConsoleExt.WriteLineWithPretext(AnimeListViewModel.DynamicAnimeItemGrid.Count, ConsoleExt.OutputType.Info);
        AnimeTypeTabControl.SelectionChanged += AnimeTypeTabControl_SelectionChanged;
        AnimeListViewModel.AnimesToGetImagesFor.CollectionChanged += OnCollectionChanged;
        Timer.Elapsed += GetImagesForViewableAnimes;
        Timer.AutoReset = false; // Timer should not automatically reset, we will reset it manually
        Task.Run(InitializeAsync);
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
            AnimeListViewModel.AnimesToGetImagesFor.Add(animeDisplayItem.AnimeId);
        }
    }

    private void AnimeItemsControl_OnElementClearing(object? sender, ItemsRepeaterElementClearingEventArgs e)
    {
        if (e.Element is Button { DataContext: AnimeDisplayItem animeDisplayItem })
        {
            AnimeListViewModel.AnimesToGetImagesFor.Remove(animeDisplayItem.AnimeId);
        }
    }
    
    private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add) return;
        // Add new items to the buffer
        _buffer.AddRange(e.NewItems.OfType<long?>());

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
        ConsoleExt.WriteLineWithPretext(itemsThatNeedImages.Count, ConsoleExt.OutputType.Info);
        var result = await SqlDbHandler.GetAnimeBitmapImagesByIds(itemsThatNeedImages);
            
        foreach (var item in AnimeListViewModel.DynamicAnimeItemGrid)
        {
            if (!result.TryGetValue(item.AnimeId, out var imagesSet)) continue;
            if (imagesSet.JPG.ImageBitmap != null)
            {
                item.AnimeImage ??= new Bitmap(new MemoryStream(imagesSet.JPG.ImageBitmap));
            }
        }
    }
}