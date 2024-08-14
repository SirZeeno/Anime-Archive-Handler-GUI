using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using FFMpegCore;
using FluentAvalonia.UI.Controls;
using LibVLCSharp.Shared;

namespace Anime_Archive_Handler_GUI.Views;

public partial class AnimeDetailControl : UserControl
{
    private LibVLC _libVlc;
    private Media? _media;
    
    private readonly string _trailerUrl;
    
    private readonly Action _navigateToAnimeDisplayList;
    AnimeDetailViewModel AnimeDetailViewModel => (DataContext as AnimeDetailViewModel)!;
    public AnimeDetailControl(AnimeDto? anime, Action navigation)
    {
        InitializeComponent();
        InitializeVlc();
        _navigateToAnimeDisplayList = navigation;
        AnimeDetailViewModel.AnimeToDisplay = anime;
        _trailerUrl = anime?.Trailer.Url ?? string.Empty;

        //GetTrailerThumbnail();
        //using var stream = new MemoryStream(anime.ImageBitmaps.JPG.ImageBitmap);
        //AnimeDetailViewModel.TrailerThumbnail = new Bitmap(stream);
        
        // Handle slider changes
        ProgressSlider.PropertyChanged += ScrubSlider_PropertyChanged;
        VolumeSlider.PropertyChanged += VolumeSlider_PropertyChanged;
        
        // Update slider as video plays
        AnimeDetailViewModel.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
    }
    
    private void BackClicked(object sender, RoutedEventArgs e)
    {
        AnimeDetailViewModel.MediaPlayer.Stop();
        _media?.Dispose();
        _libVlc.Dispose();
        _navigateToAnimeDisplayList();
    }
    
    private void InitializeVlc()
    {
        Core.Initialize();

        _libVlc = new LibVLC();
        AnimeDetailViewModel.MediaPlayer = new MediaPlayer(_libVlc)
        {
            EnableHardwareDecoding = true
        };
    }

    private async void GetTrailerThumbnail() // this is quite slow and i am thinking it might be better to just use the anime banner bitmap
    {
        var tempfile2 = Path.Combine(@"F:\Rider Projects\Anime Archive Handler GUI\Anime Archive Handler GUI\Anime Archive Handler GUI\Anime Archive Handler GUI\Databases\TempFiles", Guid.NewGuid().ToString());
            
        // Start yt-dlp process to get the media stream
        var ytDlpProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = @"F:\Rider Projects\Anime Archive Handler GUI\Anime Archive Handler GUI\Anime Archive Handler GUI\Anime Archive Handler GUI\External Dependencies\yt-dlp.exe",
                Arguments = $"-o \"{tempfile2}\" --write-thumbnail --skip-download \"{_trailerUrl}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        ytDlpProcess.Start();
        await ytDlpProcess.WaitForExitAsync();
    }


    private async void PlayTrailerFromStreamAsync()
    {
        try
        {
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
            await outputStream.CopyToAsync(memStream);
            memStream.Position = 0;
            // Create a media object from the stream
            using var media = new Media(_libVlc, new StreamMediaInput(memStream));
            // Set the media to the player and start playback
            AnimeDetailViewModel.MediaPlayer.Play(media);

            // Wait for the process to complete
            await ytDlpProcess.WaitForExitAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void PlayVideo_Click(object sender, RoutedEventArgs e)
    {
        PlayTrailerFromStreamAsync();
    }
    
    private CancellationTokenSource _seekDebounceCts;

    private void ScrubSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == RangeBase.ValueProperty)
        {
            _seekDebounceCts?.Cancel();
            _seekDebounceCts = new CancellationTokenSource();

            var newPosition = (float)(double)e.NewValue / 100f;
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
    
    private void VolumeSlider_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == RangeBase.ValueProperty)
        {
            var newVolume = (int)(double)e.NewValue;
            AnimeDetailViewModel.MediaPlayer.Volume = newVolume;
        }
    }

    private void MediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
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
    
    private void MuteOrUnmute(object? sender, RoutedEventArgs routedEventArgs)
    {
        AnimeDetailViewModel.MediaPlayer.Mute = !AnimeDetailViewModel.MediaPlayer.Mute;

        SpeakerButton.Symbol = AnimeDetailViewModel.MediaPlayer.Mute ? Symbol.Speaker2 : Symbol.SpeakerOff;
    }

    private void PlayOrPause(object? sender, RoutedEventArgs routedEventArgs)
    {
        PlaySymbol.Symbol = AnimeDetailViewModel.MediaPlayer.IsPlaying ? Symbol.Pause : Symbol.Play;
        
        if (AnimeDetailViewModel.MediaPlayer.IsPlaying)
        {
            AnimeDetailViewModel.MediaPlayer.Pause();
        }
        else
        {
            AnimeDetailViewModel.MediaPlayer.Play();
        }
    }
}