using System;
using System.Linq;
using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Anime_Archive_Handler_GUI.Views;

public partial class AnimeDetailControl : UserControl
{
    private readonly Action _navigateToAnimeDisplayList;
    AnimeDetailViewModel AnimeDetailViewModel => (DataContext as AnimeDetailViewModel)!;
    public AnimeDetailControl(AnimeDto? anime, Action navigation)
    {
        InitializeComponent();
        _navigateToAnimeDisplayList = navigation;
        AnimeDetailViewModel.AnimeToDisplay = anime;
    }
    
    private void BackClicked(object sender, RoutedEventArgs e)
    {
        _navigateToAnimeDisplayList();
    }
}