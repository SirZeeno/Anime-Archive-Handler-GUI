using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using LibVLCSharp.Shared;
using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class AnimeDetailViewModel : ViewModelBase, IDisposable
{
    private EpisodeDisplayItem? _episodeDisplayItem;
    public EpisodeDisplayItem? EpisodeDisplayItem
    {
        get => _episodeDisplayItem;
        set => this.RaiseAndSetIfChanged(ref _episodeDisplayItem, value);
    }
    
    private AnimeDto? _animeToDisplay;
    public AnimeDto? AnimeToDisplay
    {
        get => _animeToDisplay;
        set => this.RaiseAndSetIfChanged(ref _animeToDisplay, value);
    }
    
    private string _animeTitle = string.Empty;
    public string AnimeTitle
    {
        get => _animeTitle;
        set => this.RaiseAndSetIfChanged(ref _animeTitle, value);
    }
    
    private MediaPlayer _mediaPlayer;
    
    public MediaPlayer MediaPlayer
    {
        get => _mediaPlayer;
        set => this.RaiseAndSetIfChanged(ref _mediaPlayer, value);
    }
    
    private Bitmap _trailerThumbnail;
    
    public Bitmap TrailerThumbnail
    {
        get => _trailerThumbnail;
        set => this.RaiseAndSetIfChanged(ref _trailerThumbnail, value);
    }

    private void SelectedTitle(AnimeDto? animeDto)
    {
        string? englishTitle = animeDto?.Titles
            .Where(t => t.Type.ToLower() == "english")
            .Select(t => t.Title)
            .FirstOrDefault();
        string? defaultTitle = animeDto?.Titles
            .Where(t => t.Type.ToLower() == "default")
            .Select(t => t.Title)
            .FirstOrDefault();
        string title = englishTitle ?? defaultTitle ?? string.Empty;
        ConsoleExt.WriteLineWithPretext("Selected Title: " + title);
        AnimeTitle = title;
    }
    
    public ICommand OpenLinkCommand { get; }
    
    private void OpenLink(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }

    public AnimeDetailViewModel()
    {
        this.WhenAnyValue(x => x.AnimeToDisplay).Where(newAnime => newAnime != null).Subscribe(SelectedTitle);
        OpenLinkCommand = ReactiveCommand.Create<string>(OpenLink);
    }
    
    public void Dispose()
    {
        MediaPlayer.Stop();
        MediaPlayer.Dispose();
    }
}