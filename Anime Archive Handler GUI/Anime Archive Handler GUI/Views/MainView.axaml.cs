using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.Markup.Xaml.Templates;

namespace Anime_Archive_Handler_GUI.Views;

using ViewModels;

public partial class MainView : UserControl
{
    private ObservableCollection<YourResultType> SearchResults { get; } = new();

    public MainView()
    {
        InitializeComponent();
        //this.GetObservable(BoundsProperty).Subscribe(_ => AdjustGridLayout());
        AnimeCategoryTabControl.SelectionChanged += HeaderTabControl_SelectionChanged;
        HomeButton.Click += SetTabIndexToHome;
        LoadHomePage();
        //AdjustGridLayout();
        //ChangeStatus(Language.Sub, 12, 12, "Cowboy Bebop"); // needs to get fixed to get working again
    }

    private void LoadHomePage()
    {
        AnimeCategoryTabControl.SelectedIndex = 1;
        AnimeCategoryTabControl.SelectedIndex = 0;
    }

    private void SearchBox_KeyUp(object sender, KeyEventArgs e)
    {
        SearchResults.Clear(); // Clear previous results
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
                AnimeListControlView.Content = new AnimeListControl();
                break;
            case 1:
                break;
            case 2:
                ConsoleExt.WriteLineWithPretext("Tab 2 selected", ConsoleExt.OutputType.Info);
                break;
            case 3:
                AnimeItemDisplayControl.SetGridItems();//.OnCompleted(AdjustGridLayout);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
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
                break;
            case "CopyButton":
                break;
            case "PasteButton":
                
                break;
        }
    }
}