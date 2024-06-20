using System;
using System.Collections.ObjectModel;
using Avalonia;
using System.Linq;
using Avalonia.Media;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Tests;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.Markup.Xaml.Templates;
using DynamicData;
using FluentAvalonia.UI.Controls;

namespace Anime_Archive_Handler_GUI.Views;
using static Helpers.DailyFeatured;
using ViewModels;

public partial class MainView : UserControl
{
    private ObservableCollection<YourResultType> SearchResults { get; } = new ObservableCollection<YourResultType>();
    private Grid AnimeDynamicGrid;


    public MainView()
    {
        InitializeComponent();
        MainViewModel.AnimePreviewItems = new ObservableCollection<CarouselItem>();
        DataContext = new MainViewModel(); // Only for example purposes
        AnimeItemDisplayControl.MainViewInstance = this;
        AnimeItemDisplayControl.AnimeItemsControlInstance = AnimeItemsControl;
        AnimeItemDisplayControl.SetGridItems().OnCompleted(AdjustGridLayout);
        this.GetObservable(BoundsProperty).Subscribe(_ => AdjustGridLayout());
        AnimeCategoryTabControl.SelectionChanged += HeaderTabControl_SelectionChanged;
        AnimeTypeTabControl.SelectionChanged += AnimeTypeTabControl_SelectionChanged;
        HomeButton.Click += SetTabIndexToHome;
        Task.Run(InitializeAsync);
        LoadHomePage();
        AdjustGridLayout();
        ChangeStatus(Language.Sub, 12, 12, "Cowboy Bebop");
    }

    private void LoadHomePage()
    {
        AnimeCategoryTabControl.SelectedIndex = 1;
        AnimeCategoryTabControl.SelectedIndex = 0;
    }

    private async Task InitializeAsync()
    {
        var featuredItems = await PickDailyFeatured();
        await Task.Run(() =>
        {
            if (MainViewModel.AnimePreviewItems != null) MainViewModel.AnimePreviewItems!.Add(featuredItems);
        });
        //MainViewModel.AnimePreviewItems = new ObservableCollection<CarouselItem> { featuredItems };
        ConsoleExt.WriteLineWithPretext("Added Daily Featured", ConsoleExt.OutputType.Info);
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
        AnimeItemDisplayControl.AdjustGridLayout(AnimeItemsControl);
    }

    private void ChangeStatus(Language subOrDub, int subEpisodeCount, int dubEpisodeCount, string animeName)
    {
        var matches = AnimeItemsControl.Items.OfType<Border>().Where(b => b.Name == animeName);
        
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
            //DynamicGrid.RowDefinitions.Add(rowDefinition);
        }
    }
    
    private void HeaderTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Get the selected tab item
        TabItem selectedTab = (TabItem)AnimeCategoryTabControl.SelectedItem;

        switch (AnimeCategoryTabControl.SelectedIndex)
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
                ConsoleExt.WriteLineWithPretext("Tab 2 selected", ConsoleExt.OutputType.Info);
                break;
            case 3:
                AnimeItemDisplayControl.SetGridItems().OnCompleted(AdjustGridLayout);
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
        AnimeCategoryTabControl.SelectedIndex = 0;
    }

    public void HeaderButtonsClickHandler(object sender, RoutedEventArgs e)
    {
        var source = e.Source as Control;
        switch (source?.Name)
        {
            case "ImportWindowButton":
                new ImportView {DataContext = new ImportViewModel()}.Show();
                break;
            case "ImportFoldersButton":
                AnimeItemDisplayControl.UserAddAnimeToAnimeGrid();
                break;
            case "ImportFilesButton":
                AnimeItemDisplayControl.UserAddAnimeEpisodeToAnimeGrid();
                break;
            case "ExportButton":
                break;
            case "PreferencesButton":
                new SettingsView {DataContext = new SettingsViewModel()}.Show();
                break;
            case "HelpButton":
                TestStarter.StartTest();
                break;
            case "CopyButton":
                TestStarter.PortDatabase();
                break;
            case "PasteButton":
                break;
        }
    }
}