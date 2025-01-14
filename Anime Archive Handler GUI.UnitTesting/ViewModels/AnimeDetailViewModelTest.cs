using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia.Headless.NUnit;
using FluentAvalonia.UI.Controls;
using LibVLCSharp.Shared;

namespace Anime_Archive_Handler_GUI.UnitTesting.ViewModels;

[TestFixture]
[TestOf(typeof(AnimeDetailViewModel))]
public class AnimeDetailViewModelTest
{
    private readonly AnimeDetailViewModel _viewModel = new();
    private readonly MediaPlayer _mediaPlayer = new(new LibVLC());

    [SetUp]
    public void AnimeDetailViewModelSetup()
    {
        _viewModel.MediaPlayer = _mediaPlayer;
    }
    
    [AvaloniaTest]
    public void MuteButtonTest()
    {
        if (_viewModel.IsMuted)
        {
            Assert.That(_viewModel.MuteUnmuteSymbol, Is.EqualTo(Symbol.SpeakerOff)); // If it's muted, the symbol should be SpeakerOff
        }
        else if (_viewModel.IsMuted)
        {
            Assert.That(_viewModel.MuteUnmuteSymbol, Is.EqualTo(Symbol.Speaker2)); // If it's not muted, the symbol should be Speaker2
        }
        _viewModel.MuteCommand.Execute(null); // Execute the mute command
        if (_viewModel.IsMuted)
        {
            Assert.That(_viewModel.MuteUnmuteSymbol, Is.EqualTo(Symbol.SpeakerOff)); // If it's muted, the symbol should be SpeakerOff
        }
        else if (_viewModel.IsMuted)
        {
            Assert.That(_viewModel.MuteUnmuteSymbol, Is.EqualTo(Symbol.Speaker2)); // If it's not muted, the symbol should be Speaker2
        }
    }
    
    [AvaloniaTest]
    public void PlayPauseButtonTest() // make sure I assign something to play, so it can test this button
    {
        if (_mediaPlayer.IsPlaying)
        {
            Assert.That(_viewModel.PlayPauseSymbol, Is.EqualTo(Symbol.Play)); // If it's playing, the symbol should be play
        }
        else if (!_mediaPlayer.IsPlaying)
        {
            Assert.That(_viewModel.PlayPauseSymbol, Is.EqualTo(Symbol.Pause)); // If it's not playing, the symbol should be Pause
        }
        _viewModel.PlayPauseCommand.Execute(null); // Execute the mute command
        if (_mediaPlayer.IsPlaying)
        {
            Assert.That(_viewModel.PlayPauseSymbol, Is.EqualTo(Symbol.Play)); // If it's playing, the symbol should be play
        }
        else if (!_mediaPlayer.IsPlaying)
        {
            Assert.That(_viewModel.PlayPauseSymbol, Is.EqualTo(Symbol.Pause)); // If it's not playing, the symbol should be Pause
        }
    }
}