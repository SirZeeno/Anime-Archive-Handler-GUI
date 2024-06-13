using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class ImportViewModel : ViewModelBase
{
    private string? _inputPath;
    private bool _hasMultipleInOneFolder;
    private bool _hasSeasonFolders;
    private bool _isOva;
    private bool _isMovie;
    private ImportType _selectedOption;
    
    public static ObservableCollection<ImportSettings> SelectedPathDisplay { get; set; } = [];
    // Observable collection to check the item count on SelectedPathDisplay
    private ObservableCollection<ImportSettings> Items => SelectedPathDisplay;
    public static ObservableCollection<AnimeImportDisplayItem> AnimeSearchItemResultGrid { get; set; } = [];
    public static ItemsControl AnimeItemsControlInstance { get; set; } = null!;
    
    public ICommand BrowseFoldersCommand { get; }
    public ICommand BrowseFilesCommand { get; }
    public ICommand AddPathToQueueCommand { get; }
    public ICommand StartScanCommand { get; set; }
    public ReactiveCommand<ImportSettings, Unit> RemovePathFromQueueCommand { get; }
    //public ICommand RemovePathFromQueueCommand(ImportSettings item) { get; } // need to figure out how i can pass the items itself to the command so its easier to remove it from the collection
    public ImportType SelectedOption
    {
        get => _selectedOption;
        set => this.RaiseAndSetIfChanged(ref _selectedOption, value);
    }
    
    public bool HasMultipleAnimeInOneFolder
    {
        get => _hasMultipleInOneFolder; 
        set => this.RaiseAndSetIfChanged(ref _hasMultipleInOneFolder, value);
    }
    
    public bool HasSeasonFolders
    {
        get => _hasSeasonFolders; 
        set => this.RaiseAndSetIfChanged(ref _hasSeasonFolders, value);
    }
    public bool IsOva
    {
        get => _isOva; 
        set => this.RaiseAndSetIfChanged(ref _isOva, value);
    }
    public bool IsMovie
    {
        get => _isMovie; 
        set => this.RaiseAndSetIfChanged(ref _isMovie, value);
    }
    
    public string? PathTextBox
    {
        get => _inputPath;
        set => this.RaiseAndSetIfChanged(ref _inputPath, value);
    }
    
    void RemoveItem(ImportSettings item)
    {
        SelectedPathDisplay.Remove(item);
        ConsoleExt.WriteLineWithPretext($"Removed Path: '{item.SelectedPath}'", ConsoleExt.OutputType.Info);
    }

    public ImportViewModel()
    {
        SelectedOption = ImportType.Anime;
        IObservable<bool> canExecuteAdd = this.WhenAnyValue(vm => vm.PathTextBox, (path) => !string.IsNullOrEmpty(path));
        IObservable<bool> canExecuteScan = this.WhenAnyValue(x => x.Items.Count).Select(count => count > 0);
        AddPathToQueueCommand = ReactiveCommand.Create<string?>(path => ImportHandler.AddPathToQueue(new ImportSettings(path, HasMultipleAnimeInOneFolder, HasSeasonFolders, IsOva, IsMovie, SelectedOption), SelectedPathDisplay), canExecuteAdd);
        StartScanCommand = ReactiveCommand.Create(() => ImportHandler.ScanPath(SelectedPathDisplay), canExecuteScan);
        BrowseFoldersCommand = ReactiveCommand.Create(() => ImportHandler.BrowseFolders(new ImportSettings(String.Empty, HasMultipleAnimeInOneFolder, HasSeasonFolders, IsOva, IsMovie, SelectedOption)));
        BrowseFilesCommand = ReactiveCommand.Create(() => ImportHandler.BrowseFiles(new ImportSettings(String.Empty, HasMultipleAnimeInOneFolder, HasSeasonFolders, IsOva, IsMovie, SelectedOption)));
        RemovePathFromQueueCommand = ReactiveCommand.Create<ImportSettings>(RemoveItem); // no command is being set for some reason
    }
}