using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using System.Linq;
using Avalonia.Media;
using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Avalonia.Controls.Primitives;
using DynamicData;

namespace Anime_Archive_Handler_GUI.Views;
using static Helpers.DailyFeatured;
using static Helpers.ImageHelper;
using ViewModels;
using Helpers;

public partial class MainView : UserControl
{
    private const int PaddingThickness = 10;
    private const int ImageMaxWidth = 225;
    private const int ImageMaxHeight = 335;
    private const int TotalImageWidth = ImageMaxWidth + PaddingThickness * 2; // Responsible for the column spacing that each square of the grid takes
    private const int TotalImageHeight = ImageMaxHeight + PaddingThickness * 2; // Responsible for the row spacing that each square of the grid takes
    private const int AnimeListViewColor = 32;
    public ObservableCollection<YourResultType> SearchResults { get; } = new ObservableCollection<YourResultType>();


    public MainView()
    {
        InitializeComponent();
        MainViewModel.AnimePreviewItems = new ObservableCollection<CarouselItem>(); 
        this.GetObservable(BoundsProperty).Subscribe(_ => AdjustGridLayout());
        LoadImageAsync("https://cdn.myanimelist.net/images/anime/4/19644l.jpg");
        DataContext = this; // Only for example purposes, consider proper MVVM practices
        //AddColumnToAnimeList("https://cdn.myanimelist.net/images/anime/4/19644l.jpg", "Cowboy Bebop", 20); // Add three columns with an image in each
        AdjustGridLayout();
        Dispatcher.UIThread.InvokeAsync(InitializeAsync);
    }
    
    private async Task InitializeAsync()
    {
        var featuredItems = await PickDailyFeatured();
        Dispatcher.UIThread.InvokeAsync(() => MainViewModel.AnimePreviewItems.Add(featuredItems));
        //MainViewModel.AnimePreviewItems = new ObservableCollection<CarouselItem> { featuredItems };
        ConsoleExt.WriteLineWithPretext("Added Daily Featured", ConsoleExt.OutputType.Info);
    }
    
    private async void LoadImageAsync(string imageUrl)
    {
        try
        {
            using var client = new HttpClient();
            // Download the image data
            var response = await client.GetAsync(imageUrl);
            response.EnsureSuccessStatusCode();

            // Read the image data into a stream
            await using var stream = await response.Content.ReadAsStreamAsync();
            // Load the stream into a Bitmap
            var bitmap = new Bitmap(stream);

            // Set the Source of the Image control
            this.FindControl<Image>("OnlineImage1")!.Source = bitmap;
            this.FindControl<Image>("Featured1BgImg")!.Source = SkBitmapToAvaloniaBitmap(ApplyBlurEffect(ConvertToSkBitmap(bitmap)));
            this.FindControl<Image>("Featured1Img")!.Source = bitmap;
            this.FindControl<Image>("Featured2BgImg")!.Source = SkBitmapToAvaloniaBitmap(ApplyBlurEffect(ConvertToSkBitmap(bitmap)));
            this.FindControl<Image>("Featured2Img")!.Source = bitmap;
            this.FindControl<Image>("Featured3BgImg")!.Source = SkBitmapToAvaloniaBitmap(ApplyBlurEffect(ConvertToSkBitmap(bitmap)));
            this.FindControl<Image>("Featured3Img")!.Source = bitmap;
            this.FindControl<Image>("Featured4BgImg")!.Source = SkBitmapToAvaloniaBitmap(ApplyBlurEffect(ConvertToSkBitmap(bitmap)));
            this.FindControl<Image>("Featured4Img")!.Source = bitmap;
            this.FindControl<Image>("Featured5BgImg")!.Source = SkBitmapToAvaloniaBitmap(ApplyBlurEffect(ConvertToSkBitmap(bitmap)));
            this.FindControl<Image>("Featured5Img")!.Source = bitmap;
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine("Exception occurred while loading image: " + ex.Message);
        }
    }

    private void SearchBox_KeyUp(object sender, KeyEventArgs e)
    {
        SearchResults.Clear(); // Clear previous results
    }
    /*
    private void SearchBox_KeyUp(object sender, KeyEventArgs e)
    {
        SearchResults.Clear(); // Clear previous results
        var searchText = SearchBox.Text;

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            // Perform your search here and populate SearchResults
            // Example: SearchResults.Add(new YourResultType(...));

            // Assuming you have a mechanism to perform the search and obtain results
            var results = PerformSearch(searchText);
            foreach (var result in results)
            {
                SearchResults.Add(result);
            }

            SearchResultsPopup.IsOpen = SearchResults.Any();
        }
        else
        {
            SearchResultsPopup.IsOpen = false;
        }
    }

    private IEnumerable<YourResultType> PerformSearch(string searchText)
    {
        throw new NotImplementedException();
    }
    */


    private void AdjustGridLayout()
    {
        // Make sure to use the correct total width for each image including padding
        var availableWidth = DynamicScrollViewer.Bounds.Width;
        // Calculate the number of columns based on available width and total width per image
        int columns = Math.Max(1, (int)(availableWidth / (TotalImageWidth)));

        // Calculate the required number of rows based on the total number of children and columns
        int totalImages = DynamicGrid.Children.Count;
        var rows = (totalImages + columns - 1) / columns; // Ceiling of division

        UpdateGridDefinitions(columns, rows);
        RearrangeGridItems(columns);
    }

    private void UpdateGridDefinitions(int columns, int rows)
    {
        DynamicGrid.ColumnDefinitions.Clear();
        for (int col = 0; col < columns; col++)
        {
            DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        DynamicGrid.RowDefinitions.Clear();
        for (int row = 0; row < rows; row++)
        {
            DynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }
    }


    private void RearrangeGridItems(int columns)
    {
        var textBlock = new TextBlock()
        {
            Text = "No anime found",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        // Remove the TextBlock from the grid if it exists
        DynamicGrid.Children.Remove(textBlock);
        
        for (int i = 0; i < DynamicGrid.Children.Count; i++)
        {
            var child = DynamicGrid.Children[i];
            Grid.SetColumn(child, i % columns);
            Grid.SetRow(child, i / columns);
        }

        if (DynamicGrid.Children.Count != 0) return;
        DynamicGrid.Children.Add(textBlock);
    }
    
    private void AddColumnToAnimeList(string animeImageUrl, string animeName, int columnCount = 1)
    {
        for (int i = 0; i < columnCount; i++)
        {
            var columnDefinition = new ColumnDefinition { Width = new GridLength(TotalImageWidth, GridUnitType.Pixel) };
            DynamicGrid.ColumnDefinitions.Add(columnDefinition);

            var linearGradientBrush = new LinearGradientBrush
            {
                StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                EndPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
                GradientStops =
                [
                    new GradientStop(Color.FromArgb(0, 0, 0, 0), 0), // Fully transparent at the top
                    new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.6), // Fully transparent at the top
                    new GradientStop(Color.FromArgb(150, AnimeListViewColor, AnimeListViewColor, AnimeListViewColor), 0.75),
                    new GradientStop(Color.FromArgb(225, AnimeListViewColor, AnimeListViewColor, AnimeListViewColor), 0.85),
                    new GradientStop(Color.FromArgb(235, AnimeListViewColor, AnimeListViewColor, AnimeListViewColor), 0.9),
                    new GradientStop(Color.FromArgb(255, AnimeListViewColor, AnimeListViewColor, AnimeListViewColor), 0.98) // Replace with your border's background color, fully opaque at the bottom
                ]
            };
            
            var innerGrid = new Grid();
            // Define rows within the inner grid
            innerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // For the image
            innerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // For the text

            var image = new Image
            {
                MaxWidth = ImageMaxWidth,
                MaxHeight = ImageMaxHeight,
                MinHeight = ImageMaxHeight,
                MinWidth = ImageMaxWidth,
                Stretch = Stretch.UniformToFill,
                Source = ImageHelper.LoadFromResource("Assets/19644l.jpg")
            };
            
            // Create the overlay with a gradient that fades to the border's background color
            var gradientOverlay = new Border
            {
                Background = linearGradientBrush,
                Height = image.Height, // Set the overlay to be the same size as the image
            };

            gradientOverlay.Height = image.Height;
            gradientOverlay.Width = image.Width;

            // Create a panel to hold both the image and the overlay
            var panel = new Panel();
            Grid.SetRow(panel, 0); // Place the panel in the first row

            // Add the image and the overlay to the panel
            panel.Children.Add(image);
            panel.Children.Add(gradientOverlay);

            var textBlock = new TextBlock
            {
                Text = "Anime Name",
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10),
            };
            Grid.SetRow(textBlock, 2); // Place the text block in the third row

            // Add the image and text block to the inner grid
            innerGrid.Children.Add(panel);
            innerGrid.Children.Add(textBlock);

            var border = new Border
            {
                CornerRadius = new CornerRadius(5), // Corner radius of the border
                ClipToBounds = true,
                Child = innerGrid,
                Margin = new Thickness(PaddingThickness, PaddingThickness, PaddingThickness, PaddingThickness),
                Background = new SolidColorBrush(new Color(255, AnimeListViewColor, AnimeListViewColor, AnimeListViewColor)), // Background color of the border
            };

            // Set the border (with its content) to the newly added column
            Grid.SetColumn(border, DynamicGrid.ColumnDefinitions.Count - 1);
            // Assuming you want the border to span the entire first row of the outer grid
            Grid.SetRow(border, 0);
            DynamicGrid.Children.Add(border);
        }
    }

    private void AddRowToAnimeList(int rowCount = 1)
    {
        for (int i = 0; i < rowCount; i++)
        {
            var rowDefinition = new RowDefinition { Height = GridLength.Auto };
            DynamicGrid.RowDefinitions.Add(rowDefinition);
        }
    }
    
    private T FindElementByName<T>(Control control, string name) where T : Control
    {
        switch (control)
        {
            case T element when element.Name == name:
                return element;
            case ContentControl contentControl when contentControl.Content is Control content:
            {
                T foundElement = FindElementByName<T>(content, name);
                return foundElement;
            }
            case ItemsControl itemsControl:
            {
                foreach (object item in itemsControl.Items)
                {
                    if (item is not Control itemControl) continue;
                    T foundElement = FindElementByName<T>(itemControl, name);
                    return foundElement;
                }

                break;
            }
        }

        return null;
    }


    public void Next(object source, RoutedEventArgs args)
    {
        var Slides = ContentControl.FindControl<Carousel>("Slides");
        if (Slides.SelectedItem == Slides.Items.Last())
        {
            Slides.SelectedItem = Slides.Items.First();
        }
        else
        {
            Slides.Next();
        }
    }

    public void Previous(object source, RoutedEventArgs args) 
    {
        var Slides = ContentControl.FindControl<Carousel>("Slides");
        if (Slides.SelectedItem == Slides.Items.First())
        {
            Slides.SelectedItem = Slides.Items.Last();
        }
        else
        {
            Slides.Previous();
        }
    }
}