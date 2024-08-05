using System;
using System.Diagnostics;
using System.IO;
using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
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

        GetTrailerThumbnail();
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
                    Arguments = $"-o - \"{_trailerUrl}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            ytDlpProcess.Start();

            // Get the output stream from yt-dlp
            await using var outputStream = ytDlpProcess.StandardOutput.BaseStream;
            // Create a media object from the stream
            using var media = new Media(_libVlc, new StreamMediaInput(outputStream));
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
}