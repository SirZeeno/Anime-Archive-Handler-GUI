using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public abstract class PopupViewModel : ViewModelBase
{
    private string? _inputPath;
    
    public string? PathTextBox
    {
        get => _inputPath;
        set => this.RaiseAndSetIfChanged(ref _inputPath, value);
    }
}