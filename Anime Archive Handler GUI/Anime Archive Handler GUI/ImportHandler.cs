using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Database_Handeling;
using Anime_Archive_Handler_GUI.ViewModels;
using Anime_Archive_Handler_GUI.Views;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Humanizer;

namespace Anime_Archive_Handler_GUI;

public static class ImportHandler
{
    public static ImportView ImportWindowInstance { get; set; } = null!;
    
    public static async Task BrowseFolders(ImportSettings importSettings)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(ImportWindowInstance);
        
        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFolderPickerAsync(new()
        {
            Title = "Open Anime Folder(s)",
            AllowMultiple = true
        });
        
        foreach (var file in files)
        {
            AddPathToQueue(new ImportSettings(file.Path.AbsolutePath, importSettings.HasMultipleInOneFolder, importSettings.HasSeasonFolders, importSettings.IsOva, importSettings.IsMovie, importSettings.ImportType), ImportViewModel.SelectedPathDisplay); //This is temp until I figure out how to handle this new situation
        }
    }
    
    public static async Task BrowseFiles(ImportSettings importSettings)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(ImportWindowInstance);
        
        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new()
        {
            Title = "Open Anime File(s)",
            AllowMultiple = true
        });
        
        foreach (var file in files)
        {
            AddPathToQueue(new ImportSettings(file.Path.AbsolutePath, importSettings.HasMultipleInOneFolder, importSettings.HasSeasonFolders, importSettings.IsOva, importSettings.IsMovie, importSettings.ImportType), ImportViewModel.SelectedPathDisplay);
        }
    }

    private static bool IsFile(string? inputPath)
    {
        return File.Exists(inputPath);
    }
    
    public static void AddPathToQueue(ImportSettings? importSettings, ObservableCollection<ImportSettings> selectedPathDisplay)
    {
        if (!Path.Exists(importSettings?.SelectedPath)) return;
        selectedPathDisplay.Add(importSettings);
        ConsoleExt.WriteLineWithPretext($"Added Path: '{importSettings.SelectedPath}'", ConsoleExt.OutputType.Info);
    }
    
    public static async void ScanPath(ObservableCollection<ImportSettings> selectedPathDisplay)
    {
        ImportViewModel.AnimeSearchItemResultGrid.Clear();
        ConsoleExt.WriteLineWithPretext("Scanning Paths...", ConsoleExt.OutputType.Info);
        
        foreach (var textDisplayItem in selectedPathDisplay)
        {
            
            if (IsFile(textDisplayItem.SelectedPath))
            {
                
                ConsoleExt.WriteLineWithPretext("Done Scanning", ConsoleExt.OutputType.Info);
                return;
            }
            
            // Initial Checks
            string? path = textDisplayItem.SelectedPath;
            var charArray = textDisplayItem.SelectedPath?.ToCharArray();
            if (charArray != null && (charArray[^1] != '\\' || charArray[^1] != '/'))
            {
                path += "\\";
            }
            
            // Gets the folder information
            if (path == null) continue;
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (textDisplayItem.HasMultipleInOneFolder)
            {
                DirectoryInfo[] folders = Task.Run(() => directoryInfo.GetDirectories()).GetAwaiter().GetResult();
                var folderCount = folders.Length;
                ConsoleExt.WriteLineWithPretext($"Found {folderCount} folders in '{path}'", ConsoleExt.OutputType.Info);
                
                foreach (var folder in folders)
                {
                    string animeName = await InputStringHandler.AnitomyInfoExtractor(folder.Name);
                    ConsoleExt.WriteLineWithPretext($"Anime Name Extracted: {animeName}", ConsoleExt.OutputType.Info);
                    
                    var animeSearchResults = SqlDbHandler.GetAnimeByTitle(animeName);
                    
                    if (!animeSearchResults.Any())
                    {
                        ConsoleExt.WriteLineWithPretext($"No Anime Has been Found by the name of {animeName}", ConsoleExt.OutputType.Warning);
                        continue;
                    }
                    
                    ObservableCollection<AnimeDisplayItem> foundAnimes = new() { };
                    string title;

                    List<long?> animeIds = new();

                    foreach (var searchResult in animeSearchResults)
                    {
                        animeIds.Add(searchResult.MalId);
                    }
                    
                    Dictionary<long?, ICollection<TitleEntryDto>> animeTitles = SqlDbHandler.GetAnimeTitlesByIds(animeIds);
                    
                    // if found display anime display item in importer view to show the found anime
                    foreach (var animeSearchResult in animeSearchResults)
                    {
                        animeTitles.TryGetValue(animeSearchResult.MalId, out var titleEntries);
                        title = (titleEntries.Where(x => x.Type.ToLower() == "english").Select(x => x.Title).FirstOrDefault() ?? titleEntries.Where(x => x.Type.ToLower() == "default").Select(x => x.Title).FirstOrDefault()) ?? string.Empty;
                        ConsoleExt.WriteLineWithPretext($"Found Anime: '{title}'", ConsoleExt.OutputType.Info); //HelperClass.ExtractProperty(titleEntries.ToList(), item => item.Title)[1]
                        foundAnimes.Add(new(animeSearchResult.MalId, title, 12,12,12, Language.Dub));
                    }
                    ImportViewModel.AnimeSearchItemResultGrid.Add(new AnimeImportDisplayItem(animeName.ToUpperInvariant(), foundAnimes));
                }
                ConsoleExt.WriteLineWithPretext("Done Scanning", ConsoleExt.OutputType.Info);
            }
            else
            {
                // Gets files and folder information
                FileInfo[] files = Task.Run(() => directoryInfo.GetFiles()).GetAwaiter().GetResult();
            
                // displays the name of the folder and number of files
                int fileCount = files.Length;
                ConsoleExt.WriteLineWithPretext($"Folder Name: '{directoryInfo.Name}'", ConsoleExt.OutputType.Info);
                ConsoleExt.WriteLineWithPretext($"Found {fileCount} files in '{path}'", ConsoleExt.OutputType.Info);
            
                // Extract the folder name
                string animeName = await InputStringHandler.RemoveUnnecessaryNamePieces(directoryInfo.Name);
                ConsoleExt.WriteLineWithPretext($"Anime Name Extracted: {animeName}", ConsoleExt.OutputType.Info);

                // Search extracted folder name in database
                var animeSearchResults = SqlDbHandler.GetAnimeByTitle(animeName);

                // if not found, write waring message
                if (animeSearchResults.Count == 0)
                {
                    ConsoleExt.WriteLineWithPretext($"No Anime Has been Found by the name of {animeName}", ConsoleExt.OutputType.Warning);
                    return;
                }

                // if found display anime display item in importer view to show the found anime
                foreach (var animeSearchResult in animeSearchResults)
                {
                    var titleEntries = animeSearchResult.Titles;
                    ConsoleExt.WriteLineWithPretext($"Found Anime: '{HelperClass.ExtractProperty(titleEntries.ToList(), item => item.Title)}'", ConsoleExt.OutputType.Info);
                }
                ConsoleExt.WriteLineWithPretext("Done Scanning", ConsoleExt.OutputType.Info);
            }
        }
    }
    
    // give user options to choose the right anime and import their selected anime into the library folder
}