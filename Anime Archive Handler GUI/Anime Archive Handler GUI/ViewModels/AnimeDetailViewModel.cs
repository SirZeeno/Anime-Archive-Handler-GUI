using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using FluentAvalonia.UI.Controls;
using LibVLCSharp.Shared;
using CommunityToolkit.Mvvm.ComponentModel;

using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public partial class AnimeDetailViewModel : ObservableObject, IDisposable
{
    [ObservableProperty]
    private ObservableCollection<EpisodeDisplayItem?> _episodeDisplayItem;
    
    [ObservableProperty]
    private AnimeDto? _animeToDisplay;
    
    [ObservableProperty]
    private string _animeTitle = string.Empty;

    public LibVLC LibVlc { get; set; }
    [ObservableProperty]
    private MediaPlayer _mediaPlayer;
    
    [ObservableProperty]
    private Bitmap _trailerThumbnail;
    
    [ObservableProperty]
    private int _volumeSliderValue;
    
    [ObservableProperty]
    private float _videoProgressSliderValue;
    
    [ObservableProperty]
    private bool _isVolumeControlVisible;

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
    
    public ICommand SelectEpisodeCommand { get; }
    
    private void SelectEpisode(int episodeNumber)
    {
        
    }
    
    public ICommand MuteCommand { get; }
    
    [ObservableProperty]
    private bool _isMuted;
    
    public ICommand PlayPauseCommand { get; }
    public ICommand FullscreenToggleCommand { get; }
    
    public Symbol MuteUnmuteSymbol => IsMuted ? Symbol.SpeakerOff : Symbol.Speaker2;
    public Symbol PlayPauseSymbol => MediaPlayer.IsPlaying ? Symbol.Pause : Symbol.Play;
    public Symbol FullscreenSymbol => MediaPlayer.Fullscreen ? Symbol.FullScreenMinimize : Symbol.FullScreenMaximize; // not visually changing the symbol

    
    public AnimeDetailViewModel()
    {
        LibVlc = new LibVLC();
        MediaPlayer = new MediaPlayer(LibVlc)
        {
            EnableHardwareDecoding = true
        };
        MediaPlayer.Volume = 100;
        VolumeSliderValue = 100;
        IsMuted = MediaPlayer.Mute;
        
        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(IsMuted))
            {
                OnPropertyChanged(nameof(MuteUnmuteSymbol));
            }
        };
        
        // Subscribe to an event that indicates a change in the playback state
        MediaPlayer.Playing += (_, _) => OnPropertyChanged(nameof(PlayPauseSymbol));
        MediaPlayer.Paused += (_, _) => OnPropertyChanged(nameof(PlayPauseSymbol));
        MediaPlayer.Stopped += (_, _) => OnPropertyChanged(nameof(PlayPauseSymbol));
        
        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(MediaPlayer.Fullscreen))
            {
                OnPropertyChanged(nameof(FullscreenSymbol));
            }
        };
        
        this.WhenAnyValue(x => x.AnimeToDisplay).Where(newAnime => newAnime != null).Subscribe(SelectedTitle);
        OpenLinkCommand = ReactiveCommand.Create<string>(OpenLink);
        SelectEpisodeCommand = ReactiveCommand.Create<int>(SelectEpisode);
        MuteCommand = ReactiveCommand.Create(MuteOrUnmute);
        PlayPauseCommand = ReactiveCommand.Create(PlayOrPause);
        FullscreenToggleCommand = ReactiveCommand.Create(FullscreenToggle);
    }
    
    private void MuteOrUnmute()
    {
        MediaPlayer.Mute = !MediaPlayer.Mute;
        IsMuted = !MediaPlayer.Mute;
    }

    private void PlayOrPause()
    {
        if (MediaPlayer.IsPlaying)
        {
            MediaPlayer.Pause();
        }
        else
        {
            MediaPlayer.Play();
        }
    }

    private void FullscreenToggle()
    {
        MediaPlayer.ToggleFullscreen(); // Toggle for the fullscreen state is not working as expected
    }
    
    public void Dispose()
    {
        MediaPlayer.Stop();
        MediaPlayer.Dispose();
        LibVlc.Dispose();
    }
}