using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using LibVLCSharp.Shared;
using Media = LibVLCSharp.Shared.Media;

namespace Anime_Archive_Handler_GUI.Views;

public partial class AnimeDetailControlView : UserControl
{
    private readonly string _trailerUrl;
    
    private readonly Action _navigateToAnimeDisplayList;
    
    private readonly CancellationTokenSource? _cancellationTokenSource;
    private AnimeDetailViewModel AnimeDetailViewModel => (DataContext as AnimeDetailViewModel)!;
    public AnimeDetailControlView(AnimeDto? anime, Action navigation)
    {
        InitializeComponent();
        
        _navigateToAnimeDisplayList = navigation;
        AnimeDetailViewModel.AnimeToDisplay = anime;
        _trailerUrl = anime?.Trailer.Url ?? string.Empty;
        
        SpeakerButton.Symbol = AnimeDetailViewModel.MediaPlayer.Mute ? Symbol.SpeakerOff : Symbol.Speaker2;
        
        // Handle slider changes
        ProgressSlider.PropertyChanged += ScrubSlider_PropertyChanged;
        VolumeSlider.PropertyChanged += VolumeSlider_PropertyChanged;
        
        // Update slider as video plays
        AnimeDetailViewModel.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
        
        _cancellationTokenSource = new CancellationTokenSource();
        PlayTrailerFromStreamAsync(_cancellationTokenSource.Token);
    }

    private void BackClicked(object sender, RoutedEventArgs e)
    {
        AnimeDetailViewModel.MediaPlayer.Stop();
        _cancellationTokenSource?.Cancel();
        
        _navigateToAnimeDisplayList();
    }
    
    private async void PlayTrailerFromStreamAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(_trailerUrl))
            {
                return;
            }

            if (!HelperClass.IsValidUrl(_trailerUrl))
            {
                return;
            }

            // Check the status code of the page before starting yt-dlp
            if (await HelperClass.GetStatusCodeAsync(_trailerUrl) != HttpStatusCode.OK)
            {
                return;
            }

            // Start yt-dlp process to get the media stream
            var ytDlpProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"F:\Rider Projects\Anime Archive Handler GUI\Anime Archive Handler GUI\Anime Archive Handler GUI\Anime Archive Handler GUI\External Dependencies\yt-dlp.exe",
                    Arguments = $"-o - --buffer-size 10000 \"{_trailerUrl}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            ytDlpProcess.Start();

            // Get the output stream from yt-dlp
            await using var outputStream = ytDlpProcess.StandardOutput.BaseStream;
            MemoryStream memStream = new MemoryStream();
            
            // Copy the stream asynchronously, supporting cancellation
            await outputStream.CopyToAsync(memStream, 81920, cancellationToken);
            memStream.Position = 0;

            // Create a media object from the stream
            using var media = new Media(AnimeDetailViewModel.LibVlc, new StreamMediaInput(memStream));
            
            // Set the media to the player and start playback
            AnimeDetailViewModel.MediaPlayer.Media = media;

            // Monitor cancellation token to stop playback if requested
            cancellationToken.Register(() =>
            {
                if (!ytDlpProcess.HasExited)
                {
                    ytDlpProcess.Kill();
                }
            });

            // Wait for the process to complete or for cancellation
            await ytDlpProcess.WaitForExitAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Playback was canceled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    private void PlayVideo_Click(object sender, RoutedEventArgs e)
    {
        using var media = new Media(AnimeDetailViewModel.LibVlc, new Uri(@"E:\TV Shows\Scott Pilgrim Takes Off\Season 1\Scott Pilgrim Takes Off - S01E01 - Scott Pilgrim's Precious Little Life WEBDL-1080p.mkv"));
        AnimeDetailViewModel.MediaPlayer.Play(media);
    }
    
    private CancellationTokenSource? _seekDebounceCts;

    private void ScrubSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == RangeBase.ValueProperty)
        {
            _seekDebounceCts?.Cancel();
            _seekDebounceCts = new CancellationTokenSource();

            var newPosition = (float)(double)e.NewValue! / 100f;
            Task.Delay(40, _seekDebounceCts.Token)
                .ContinueWith(t =>
                {
                    if (!t.IsCanceled)
                    {
                        Dispatcher.UIThread.InvokeAsync(() =>
                        {
                            AnimeDetailViewModel.MediaPlayer.Position = newPosition;
                        });
                    }
                }, TaskScheduler.Default);
        }
    }
    
    private void VolumeSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == RangeBase.ValueProperty)
        {
            var newVolume = (int)(double)e.NewValue!;
            AnimeDetailViewModel.MediaPlayer.Volume = newVolume;
        }
    }

    private void MediaPlayer_TimeChanged(object? sender, MediaPlayerTimeChangedEventArgs e)
    {
        // Use Dispatcher.UIThread.InvokeAsync to ensure the code runs on the UI thread
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            ProgressSlider.PropertyChanged -= ScrubSlider_PropertyChanged;
            AnimeDetailViewModel.VideoProgressSliderValue = AnimeDetailViewModel.MediaPlayer.Position * 100;
            ProgressSlider.PropertyChanged += ScrubSlider_PropertyChanged;
        });
    }
    
    private void VolumeControl_PointerEnter(object sender, PointerEventArgs e)
    {
        AnimeDetailViewModel.IsVolumeControlVisible = true;
    }

    private void VolumeControl_PointerLeave(object sender, PointerEventArgs e)
    {
        AnimeDetailViewModel.IsVolumeControlVisible = false;
    }
}