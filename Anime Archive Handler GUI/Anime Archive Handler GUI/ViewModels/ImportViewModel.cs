using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class ImportViewModel : ViewModelBase
{
    private string? _inputPath;
    private bool _hasMultipleInOneFolder;
    
    public static ObservableCollection<TextDisplayItem> SelectedPathDisplay { get; set; } = [];
    // Observable collection to check the item count on SelectedPathDisplay
    private ObservableCollection<TextDisplayItem> Items => SelectedPathDisplay;
    public static ObservableCollection<AnimeDisplayItem> AnimeSearchItemResultGrid { get; set; } = [];
    
    public ICommand BrowseFoldersCommand { get; }
    public ICommand BrowseFilesCommand { get; }
    public ICommand AddPathToQueueCommand { get; }
    public ICommand StartScanCommand { get; }
    
    public bool HasMultipleAnimeInOneFolder
    {
        get => _hasMultipleInOneFolder; 
        set => this.RaiseAndSetIfChanged(ref _hasMultipleInOneFolder, value);
    }
    
    public string? PathTextBox
    {
        get => _inputPath;
        set => this.RaiseAndSetIfChanged(ref _inputPath, value);
    }

    public ImportViewModel()
    {
        IObservable<bool> canExecuteAdd = this.WhenAnyValue(vm => vm.PathTextBox, (path) => !string.IsNullOrEmpty(path));
        IObservable<bool> canExecuteScan = this.WhenAnyValue(x => x.Items.Count).Select(count => count > 0);
        AddPathToQueueCommand = ReactiveCommand.Create<string?>(path => ImportHandler.AddPathToQueue(path, SelectedPathDisplay), canExecuteAdd);
        StartScanCommand = ReactiveCommand.Create(() => ImportHandler.ScanPath(SelectedPathDisplay, _hasMultipleInOneFolder), canExecuteScan);
        BrowseFoldersCommand = ReactiveCommand.Create(ImportHandler.BrowseFolders);
        BrowseFilesCommand = ReactiveCommand.Create(ImportHandler.BrowseFiles);
    }
}