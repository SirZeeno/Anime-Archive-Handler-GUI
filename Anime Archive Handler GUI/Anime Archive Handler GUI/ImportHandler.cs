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

namespace Anime_Archive_Handler_GUI;

public static class ImportHandler
{
    public static ImportView ImportWindowInstance { get; set; } = null!;
    
    /// <summary>
    /// Opens a folder picker dialog to select folders for importing anime.
    /// </summary>
    /// <param name="importSettings">Import settings</param>
    public static async Task BrowseFolders(ImportSettings importSettings)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(ImportWindowInstance);
        
        // Start an async operation to open the dialog.
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
    
    /// <summary>
    /// Opens a file picker dialog to select files for importing anime.
    /// </summary>
    /// <param name="importSettings">Import settings</param>
    public static async Task BrowseFiles(ImportSettings importSettings)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(ImportWindowInstance);
        
        // Start an async operation to open the dialog.
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

    /// <summary>
    /// Checks if the given path is a file.
    /// </summary>
    /// <param name="inputPath">Path to check</param>
    /// <returns>Boolean indicating if the path is a file</returns>
    private static bool IsFile(string? inputPath)
    {
        return File.Exists(inputPath);
    }
    
    /// <summary>
    /// Adds the selected path to the queue for Scanning.
    /// </summary>
    /// <param name="importSettings">Import settings</param>
    /// <param name="selectedPathDisplay">List of selected paths</param>
    public static void AddPathToQueue(ImportSettings? importSettings, ObservableCollection<ImportSettings> selectedPathDisplay)
    {
        if (!Path.Exists(importSettings?.SelectedPath)) return;
        selectedPathDisplay.Add(importSettings);
        ConsoleExt.WriteLineWithPretext($"Added Path: '{importSettings.SelectedPath}'");
    }
    
    /// <summary>
    /// Scans the selected paths for anime and updates the UI with the results.
    /// </summary>
    /// <param name="selectedPathDisplay">List of selected paths</param>
    public static async void ScanPath(ObservableCollection<ImportSettings> selectedPathDisplay)
    {
        try
        {
            ImportViewModel.AnimeSearchItemResultGrid.Clear();
            ConsoleExt.WriteLineWithPretext("Scanning Paths...");
        
            foreach (var textDisplayItem in selectedPathDisplay)
            {
            
                if (IsFile(textDisplayItem.SelectedPath))
                {
                
                    ConsoleExt.WriteLineWithPretext("Done Scanning");
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
                    ConsoleExt.WriteLineWithPretext($"Found {folderCount} folders in '{path}'");
                
                    foreach (var folder in folders)
                    {
                        string animeName = await InputStringHandler.AnitomyInfoExtractor(folder.Name);
                        ConsoleExt.WriteLineWithPretext($"Anime Name Extracted: {animeName}");
                    
                        var animeSearchResults = SqlDbHandler.GetAnimeByTitle(animeName);
                    
                        if (!animeSearchResults.Any())
                        {
                            ConsoleExt.WriteLineWithPretext($"No Anime Has been Found by the name of {animeName}", ConsoleExt.OutputType.Warning);
                            continue;
                        }
                    
                        ObservableCollection<AnimeDisplayItem> foundAnimes = new();
                        string? title = null;

                        List<long> animeIds = new();

                        foreach (var searchResult in animeSearchResults)
                        {
                            animeIds.Add((long)searchResult.MalId!);
                        }
                    
                        Dictionary<long, ICollection<TitleEntryDto>> animeTitles = SqlDbHandler.GetAnimeTitlesByIds(animeIds);
                    
                        // if found display anime display item in importer view to show the found anime
                        foreach (var animeSearchResult in animeSearchResults)
                        {
                            animeTitles.TryGetValue((long)animeSearchResult.MalId!, out var titleEntries);
                            if (titleEntries != null)
                                title =
                                    (titleEntries.Where(x => x.Type.ToLower() == "english").Select(x => x.Title)
                                         .FirstOrDefault() ??
                                     titleEntries.Where(x => x.Type.ToLower() == "default").Select(x => x.Title)
                                         .FirstOrDefault()) ?? string.Empty;

                            if (title == null) continue;
                            ConsoleExt.WriteLineWithPretext($"Found Anime: '{title}'"); //HelperClass.ExtractProperty(titleEntries.ToList(), item => item.Title)[1]
                            foundAnimes.Add(new(animeSearchResult.MalId, title, 12, 12, 12, Language.Dub));
                        }
                        ImportViewModel.AnimeSearchItemResultGrid.Add(new AnimeImportDisplayItem(animeName.ToUpperInvariant(), foundAnimes));
                    }
                    ConsoleExt.WriteLineWithPretext("Done Scanning");
                }
                else
                {
                    // Gets files and folder information
                    FileInfo[] files = Task.Run(() => directoryInfo.GetFiles()).GetAwaiter().GetResult();
            
                    // displays the name of the folder and number of files
                    int fileCount = files.Length;
                    ConsoleExt.WriteLineWithPretext($"Folder Name: '{directoryInfo.Name}'");
                    ConsoleExt.WriteLineWithPretext($"Found {fileCount} files in '{path}'");
            
                    // Extract the folder name
                    string animeName = await InputStringHandler.RemoveUnnecessaryNamePieces(directoryInfo.Name);
                    ConsoleExt.WriteLineWithPretext($"Anime Name Extracted: {animeName}");

                    // Search extracted folder name in the database
                    var animeSearchResults = SqlDbHandler.GetAnimeByTitle(animeName);

                    // if not found, write a waring message
                    if (animeSearchResults.Count == 0)
                    {
                        ConsoleExt.WriteLineWithPretext($"No Anime Has been Found by the name of {animeName}", ConsoleExt.OutputType.Warning);
                        return;
                    }

                    // if found display anime display item in importer view to show the found anime
                    foreach (var animeSearchResult in animeSearchResults)
                    {
                        var titleEntries = animeSearchResult.Titles;
                        if (titleEntries != null)
                        {
                            ConsoleExt.WriteLineWithPretext($"Found Anime: '{HelperClass.ExtractProperty(titleEntries.ToList(), item => item.Title)}'");
                        }
                        else
                        {
                            ConsoleExt.WriteLineWithPretext("No Anime Found", ConsoleExt.OutputType.Warning);
                        }
                    
                    }
                    ConsoleExt.WriteLineWithPretext("Done Scanning");
                }
            }
        }
        catch (Exception e)
        {
            ConsoleExt.WriteLineWithPretext($"An error has occured while Scanning: {e}", ConsoleExt.OutputType.Error, e);
        }
    }
    
    // give user options to choose the right anime and import their selected anime into the library folder
}