using System.ComponentModel;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class SharedViewModel : INotifyPropertyChanged
{
    private int _selectedAnimeHeaderMenuTabIndex;
    private int _selectedAnimeTypeMenuTabIndex;

    public int SelectedAnimeHeaderMenuTabIndex
    {
        get => _selectedAnimeHeaderMenuTabIndex;
        set
        {
            if (_selectedAnimeHeaderMenuTabIndex == value) return;
            _selectedAnimeHeaderMenuTabIndex = value;
            OnPropertyChanged(nameof(SelectedAnimeHeaderMenuTabIndex));
        }
    }
    
    public int SelectedAnimeTypeMenuTabIndex
    {
        get => _selectedAnimeHeaderMenuTabIndex;
        set
        {
            if (_selectedAnimeTypeMenuTabIndex == value) return;
            _selectedAnimeTypeMenuTabIndex = value;
            OnPropertyChanged(nameof(SelectedAnimeTypeMenuTabIndex));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}