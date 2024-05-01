using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.ViewModels;
using Anime_Archive_Handler_GUI.Views;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using DynamicData;

namespace Anime_Archive_Handler_GUI;

public class AnimeItemDisplayControl
{
    public static MainView MainViewInstance { get; set; } = null!;
    public static ItemsControl AnimeItemsControlInstance { get; set; } = null!;
    public static ScrollViewer ScrollViewerInstance { get; set; } = null!;
    
    private const int PaddingThickness = 10;
    private const int ImageMaxWidth = 225;
    private const int ImageMaxHeight = 335;
    private const int TotalImageWidth = ImageMaxWidth + PaddingThickness * 2; // Responsible for the column spacing that each square of the grid takes
    private const int TotalImageHeight = ImageMaxHeight + PaddingThickness * 2; // Responsible for the row spacing that each square of the grid takes
    private const int AnimeListViewColor = 32;

    public static TaskAwaiter SetGridItems(int count = 20)
    {
        ObservableCollection<AnimeDisplayItem> animeItems = [];
        
        for (int i = 0; i < count; i++)
        {
            const int paddingThickness = 10;
            const int imageMaxWidth = 225;
            const int imageMaxHeight = 335;
            const int totalImageWidth = imageMaxWidth + paddingThickness * 2;
            var columnDefinition = new ColumnDefinition { Width = new GridLength(totalImageWidth, GridUnitType.Pixel) };
            
            var gridInstance = AnimeItemsControlInstance.ItemsPanelRoot as Grid;
            gridInstance?.ColumnDefinitions.Add(columnDefinition);
            
            animeItems.Add(new AnimeDisplayItem("https://cdn.myanimelist.net/images/anime/4/19644l.jpg", "Cowboy Bebop", 12, 12, 12, Language.Dub));
            animeItems.Add(new AnimeDisplayItem("https://cdn.myanimelist.net/images/anime/7/20310l.jpg", "Trigun", 12, 12, 12, Language.Dub));
        }
        
        ConsoleExt.WriteLineWithPretext("Picked Grid Items", ConsoleExt.OutputType.Info);
        return Dispatcher.UIThread.InvokeAsync(() => MainViewModel.DynamicAnimeItemGrid.AddRange(animeItems)).GetAwaiter();
    }
    
    public TaskAwaiter AddAnimeToAnimeGrid(AnimeDisplayItem animeItem, int count = 1)
    {
        ObservableCollection<AnimeDisplayItem> animeItems = [];
        
        for (int i = 0; i < count; i++)
        {
            animeItems.Add(animeItem);
        }
        
        ConsoleExt.WriteLineWithPretext("Added Anime Grid Items", ConsoleExt.OutputType.Info);
        return Dispatcher.UIThread.InvokeAsync(() => MainViewModel.DynamicAnimeItemGrid.AddRange(animeItems)).GetAwaiter();
    }

    internal static async Task UserAddAnimeToAnimeGrid() //TODO: Add functionality for user to add anime to grid
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(MainViewInstance);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Anime File(s)",
            AllowMultiple = true
        });
    }
    internal static async Task UserAddAnimeEpisodeToAnimeGrid() //TODO: Add functionality for user to add anime to grid
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(MainViewInstance);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Open Anime Folder(s)",
            AllowMultiple = true
        });
    }
    
    public static void AdjustGridLayout(ItemsControl itemsControl)
    {
        // Make sure to use the correct total width for each image including padding
        var availableWidth = ScrollViewerInstance.Bounds.Width;
        // Calculate the number of columns based on available width and total width per image
        int columns = Math.Max(1, (int)(availableWidth / (TotalImageWidth)));

        // Calculate the required number of rows based on the total number of children and columns
        if (itemsControl.ItemsPanelRoot is Grid testGrid)
        {
            int totalImages = itemsControl.ItemCount;
            var rows = (totalImages + columns - 1) / columns; // Ceiling of division

            UpdateGridDefinitions(columns, rows, itemsControl);
        }
        
        RearrangeGridItems(columns, itemsControl);
    }

    private static void UpdateGridDefinitions(int columns, int rows, ItemsControl itemsControl)
    {
        if (itemsControl.ItemsPanelRoot is not Grid testGrid) throw new ArgumentNullException(nameof(testGrid));
        testGrid.ColumnDefinitions.Clear();
        for (int col = 0; col < columns; col++)
        {
            testGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        testGrid.RowDefinitions.Clear();
        for (int row = 0; row < rows; row++)
        {
            testGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }
    }
    
    private static void RearrangeGridItems(int columns, ItemsControl itemsControl)
    {
        var textBlock = new TextBlock()
        {
            Name = "NoAnimeFoundTextBlock",
            Text = "No anime found",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        // Remove the TextBlock from the grid if it exists
        if (itemsControl.ItemCount > 1)
        {
            var textBlockToRemove = itemsControl.Items.OfType<TextBlock>().Where(child => child.Name == "NoAnimeFoundTextBlock");
            //DynamicGrid.Children.RemoveAll(textBlockToRemove);
            //AnimeItemsControlInstance.Items.Remove(textBlock); //need to fix this to remove from itemsource instead of items
        }
        
        for (int i = 0; i < itemsControl.ItemCount; i++)
        {
            var child = itemsControl.ContainerFromIndex(i);
            if (child == null) continue;
            Grid.SetColumn(child, i % columns);
            Grid.SetRow(child, i / columns);
        }

        // Add the "No anime found" TextBlock to the grid
        if (itemsControl.ItemCount > 0) return;
        ConsoleExt.WriteLineWithPretext("No anime found", ConsoleExt.OutputType.Info);
        //AnimeItemsControlInstance.ItemsSource = new List<TextBlock> { textBlock };
    }
}