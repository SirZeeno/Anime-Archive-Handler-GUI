using System;
using System.Collections.ObjectModel;
using Avalonia;
using System.Linq;
using Avalonia.Media;
using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.Markup.Xaml.Templates;
using DynamicData;
using FluentAvalonia.UI.Controls;

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
    private ObservableCollection<YourResultType> SearchResults { get; } = new ObservableCollection<YourResultType>();
    private Grid AnimeDynamicGrid;


    public MainView()
    {
        InitializeComponent();
        MainViewModel.AnimePreviewItems = new ObservableCollection<CarouselItem>();
        this.GetObservable(BoundsProperty).Subscribe(_ => AdjustGridLayout());
        DataContext = this; // Only for example purposes
        AnimeItemDisplayControl.MainViewInstance = this;
        AnimeItemDisplayControl.AnimeItemGridInstance = DynamicGrid;
        AnimeItemDisplayControl.SetGridItems();
        //AddColumnToAnimeList("https://cdn.myanimelist.net/images/anime/4/19644l.jpg", "Cowboy Bebop", 12, 12, Language.Dub, 20);
        //AddColumnToAnimeList("https://cdn.myanimelist.net/images/anime/7/20310l.jpg", "Trigun", 12, 12, Language.Dub, 20);
        HeaderTabControl.SelectionChanged += HeaderTabControl_SelectionChanged;
        AnimeTypeTabControl.SelectionChanged += AnimeTypeTabControl_SelectionChanged;
        HomeButton.Click += SetTabIndexToHome;
        Dispatcher.UIThread.InvokeAsync(InitializeAsync);
        LoadHomePage();
        AdjustGridLayout();
        ChangeStatus(Language.Sub, 12, 12, "Cowboy Bebop");
    }

    private void LoadHomePage()
    {
        HeaderTabControl.SelectedIndex = 1;
        HeaderTabControl.SelectedIndex = 0;
    }


    private async Task InitializeAsync()
    {
        var featuredItems = await PickDailyFeatured();
        Dispatcher.UIThread.InvokeAsync(() => MainViewModel.AnimePreviewItems.Add(featuredItems));
        //MainViewModel.AnimePreviewItems = new ObservableCollection<CarouselItem> { featuredItems };
        ConsoleExt.WriteLineWithPretext("Added Daily Featured", ConsoleExt.OutputType.Info);
    }
    
    private async void LoadImageAsync(string imageUrl) // ToDo: solve the Object reference not set issue
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
            Name = "NoAnimeFoundTextBlock",
            Text = "No anime found",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        // Remove the TextBlock from the grid if it exists
        if (DynamicGrid.Children.Count > 1)
        {
            var textBlockToRemove = DynamicGrid.Children.OfType<TextBlock>().Where(child => child.Name == "NoAnimeFoundTextBlock");
            DynamicGrid.Children.RemoveAll(textBlockToRemove);
        }

        for (int i = 0; i < DynamicGrid.Children.Count; i++)
        {
            var child = DynamicGrid.Children[i];
            Grid.SetColumn(child, i % columns);
            Grid.SetRow(child, i / columns);
        }

        // Add the "No anime found" TextBlock to the grid
        if (DynamicGrid.Children.Count > 0) return;
        ConsoleExt.WriteLineWithPretext("No anime found", ConsoleExt.OutputType.Info);
        DynamicGrid.Children.Add(textBlock);
    }
    
    public void AddColumnToAnimeList(string animeImageUrl, string animeName, int subEpisodeCount, int dubEpisodeCount, Language subOrDub = default, int columnCount = 1)
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
                Source = LoadFromWeb(animeImageUrl)
            };

            var micIcon = new SymbolIcon
            {
                Symbol = Symbol.Microphone,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            
            var cCIcon = new SymbolIcon
            {
                Symbol = Symbol.ClosedCaption,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            
            var subDubStackPanel = new StackPanel
            {
                Name = "SubDubStackPanel",
                Background = new SolidColorBrush(Colors.Transparent),
                Height = 40,
                Width = 80,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            
            var dubCounterTextBlock = new TextBlock
            {
                Text = $"{dubEpisodeCount}",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 16
            };
            
            var subCounterTextBlock = new TextBlock
            {
                Text = $"{subEpisodeCount}",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 16
            };
            
            var dubInnerStackPanel = new StackPanel
            {
                Background = new SolidColorBrush(new Color(255, 49, 49, 49)),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            
            var subInnerStackPanel = new StackPanel
            {
                Background = new SolidColorBrush(new Color(255, 49, 49, 49)),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            
            switch (subOrDub)
            {
                case Language.Dub:
                    dubInnerStackPanel.Children.AddRange(new Control[] { micIcon, dubCounterTextBlock });
                    subInnerStackPanel.Children.AddRange(new Control[] { subCounterTextBlock });
                    subInnerStackPanel.Background = new SolidColorBrush(new Color(255, 52,52,52));
                    subDubStackPanel.Children.Add(dubInnerStackPanel);
                    subDubStackPanel.Children.Add(subInnerStackPanel);
                    break;
                case Language.Sub:
                    dubInnerStackPanel.Children.AddRange(new Control[] { dubCounterTextBlock });
                    dubInnerStackPanel.Background = new SolidColorBrush(new Color(255, 52,52,52));
                    subInnerStackPanel.Children.AddRange(new Control[] { cCIcon, subCounterTextBlock });
                    subDubStackPanel.Children.Add(subInnerStackPanel);
                    subDubStackPanel.Children.Add(dubInnerStackPanel);
                    break;
                default:
                {
                    if (subOrDub == default)
                    {
                        dubInnerStackPanel.Children.AddRange(new Control[] { micIcon, dubCounterTextBlock });
                        subInnerStackPanel.Children.AddRange(new Control[] { cCIcon, subCounterTextBlock });
                        subDubStackPanel.Children.Add(subInnerStackPanel);
                        subDubStackPanel.Children.Add(dubInnerStackPanel);
                    }

                    break;
                }
            }

            // Create the overlay with a gradient that fades to the border's background color
            var gradientOverlay = new Border
            {
                Background = linearGradientBrush,
                Height = image.Height, // Set the overlay to be the same size as the image
            };

            gradientOverlay.Height = image.Height;
            gradientOverlay.Width = image.Width;

            // Create a panel to hold both the image and the overlay
            var panel = new Panel
            {
                Name = "ImagePanel"
            };
            Grid.SetRow(panel, 0); // Place the panel in the first row

            // Add the image and the overlay to the panel
            panel.Children.Add(image);
            panel.Children.Add(gradientOverlay);
            panel.Children.Add(subDubStackPanel);

            var textBlock = new TextBlock
            {
                Text = animeName,
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
                Name = animeName,
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

    private void ChangeStatus(Language subOrDub, int subEpisodeCount, int dubEpisodeCount, string animeName)
    {
        var matches = DynamicGrid.Children.OfType<Border>().Where(b => b.Name == animeName);
        
        foreach (var match in matches)
        {
            var grid = match.Child?.GetType() == typeof(Grid) ? match.Child as Grid : null;
            var panelList = grid?.Children.OfType<Panel>().Where(b => b.Name == "ImagePanel");
            if (panelList == null) continue;
            {
                foreach (var panel in panelList)
                {
                    var subDubStackPanelList =
                        panel.Children.OfType<StackPanel>().Where(b => b.Name == "SubDubStackPanel");
                    foreach (var subDubStackPanel in subDubStackPanelList)
                    {
                        var dubInnerStackPanel = new StackPanel
                        {
                            Background = new SolidColorBrush(new Color(255, 49, 49, 49)),
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(2, 0, 0, 0),
                            Orientation = Orientation.Horizontal
                        };
            
                        var subInnerStackPanel = new StackPanel
                        {
                            Background = new SolidColorBrush(new Color(255, 49, 49, 49)),
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(2, 0, 0, 0),
                            Orientation = Orientation.Horizontal
                        };

                        var micIcon = new SymbolIcon
                        {
                            Symbol = Symbol.Microphone,
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
            
                        var cCIcon = new SymbolIcon
                        {
                            Symbol = Symbol.ClosedCaption,
                            FontSize = 20,
                            Foreground = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
        
                        var dubCounterTextBlock = new TextBlock
                        {
                            Text = $"{dubEpisodeCount}",
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 16
                        };
            
                        var subCounterTextBlock = new TextBlock
                        {
                            Text = $"{subEpisodeCount}",
                            Foreground = new SolidColorBrush(Colors.White),
                            FontSize = 16
                        };
                        
                        dubInnerStackPanel.Children.Clear();
                        subInnerStackPanel.Children.Clear();
                        subDubStackPanel.Children.Clear();
                        
                        switch (subOrDub) //ToDo: rework this so it only adds the icon into each section and not 
                        {
                            case Language.Dub:
                                dubInnerStackPanel.Children.AddRange(new Control[] { micIcon, dubCounterTextBlock });
                                subInnerStackPanel.Children.AddRange(new Control[] { subCounterTextBlock });
                                subInnerStackPanel.Background = new SolidColorBrush(new Color(255, 52, 52, 52));
                                subDubStackPanel.Children.Add(dubInnerStackPanel);
                                subDubStackPanel.Children.Add(subInnerStackPanel);
                                ConsoleExt.WriteLineWithPretext("Language is Dub", ConsoleExt.OutputType.Info);
                                break;
                            case Language.Sub:
                                dubInnerStackPanel.Children.AddRange(new Control[] { dubCounterTextBlock });
                                dubInnerStackPanel.Background = new SolidColorBrush(new Color(255, 52, 52, 52));
                                subInnerStackPanel.Children.AddRange(new Control[] { cCIcon, subCounterTextBlock });
                                subDubStackPanel.Children.Add(subInnerStackPanel);
                                subDubStackPanel.Children.Add(dubInnerStackPanel);
                                ConsoleExt.WriteLineWithPretext("Language is Sub", ConsoleExt.OutputType.Info);
                                break;
                            default:
                            {
                                if (subOrDub == default)
                                {
                                    dubInnerStackPanel.Children.AddRange(new Control[]
                                        { micIcon, dubCounterTextBlock });
                                    subInnerStackPanel.Children.AddRange(new Control[] { cCIcon, subCounterTextBlock });
                                    subDubStackPanel.Children.Add(subInnerStackPanel);
                                    subDubStackPanel.Children.Add(dubInnerStackPanel);
                                    ConsoleExt.WriteLineWithPretext("Language is None", ConsoleExt.OutputType.Info);
                                }

                                break;
                            }
                        }
                    }
                }
            }
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
    
    private void HeaderTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Get the selected tab item
        TabItem selectedTab = (TabItem)HeaderTabControl.SelectedItem;

        switch (HeaderTabControl.SelectedIndex)
        {
            // Set the content of the ContentControl based on the selected tab
            case 0:
                // Load content for Tab 1
                ConsoleExt.WriteLineWithPretext("HomePage Tab selected", ConsoleExt.OutputType.Info);
                
                Dispatcher.UIThread.Invoke(() => AnimePreviewContentControl.ContentTemplate = (DataTemplate)Resources["HomePage"]!);
                Dispatcher.UIThread.Invoke(() => AnimePreviewContentControl.Content = new AnimeCarousel(MainViewModel.AnimePreviewItems));
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
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

    private void SetTabIndexToHome(object? sender, RoutedEventArgs e)
    {
        HeaderTabControl.SelectedIndex = 0;
    }
}