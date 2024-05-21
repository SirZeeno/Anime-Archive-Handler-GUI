using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.ViewModels;
using Anime_Archive_Handler_GUI.Views;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Anime_Archive_Handler_GUI;

public static class ImportHandler
{
    public static ImportView ImportWindowInstance { get; set; } = null!;
    
    public static async Task BrowseFolders()
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(ImportWindowInstance);
        
        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Open Anime Folder(s)",
            AllowMultiple = true
        });
        
        foreach (var file in files)
        {
            AddPathToQueue(file.Path.AbsolutePath, ImportViewModel.SelectedPathDisplay);
        }
    }
    
    public static async Task BrowseFiles()
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(ImportWindowInstance);
        
        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Anime File(s)",
            AllowMultiple = true
        });
        
        foreach (var file in files)
        {
            AddPathToQueue(file.Path.AbsolutePath, ImportViewModel.SelectedPathDisplay);
        }
    }

    private static bool IsFile(string? inputPath)
    {
        return File.Exists(inputPath);
    }
    
    public static void AddPathToQueue(string? inputText, ObservableCollection<TextDisplayItem> selectedPathDisplay)
    {
        if (!Path.Exists(inputText)) return;
        selectedPathDisplay.Add(new TextDisplayItem(inputText));
        ConsoleExt.WriteLineWithPretext($"Added Path: '{inputText}'", ConsoleExt.OutputType.Info);
    }
    
    public static void ScanPath(ObservableCollection<TextDisplayItem> selectedPathDisplay, bool hasMultipleInOneFolder)
    {
        ConsoleExt.WriteLineWithPretext("Scanning Paths...", ConsoleExt.OutputType.Info);
        
        foreach (var textDisplayItem in selectedPathDisplay)
        {
            
            if (IsFile(textDisplayItem.text))
            {
                
                ConsoleExt.WriteLineWithPretext("Done Scanning", ConsoleExt.OutputType.Info);
                return;
            }
            
            // Initial Checks
            string path = textDisplayItem.text;
            var charArray = textDisplayItem.text.ToCharArray();
            if (charArray[^1] != '\\' || charArray[^1] != '/')
            {
                path += "\\";
            }
            
            // Gets the folder information
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (hasMultipleInOneFolder)
            {
                DirectoryInfo[] folders = directoryInfo.GetDirectories();
                int folderCount = folders.Length;
                ConsoleExt.WriteLineWithPretext($"Found {folderCount} folders in '{path}'", ConsoleExt.OutputType.Info);
                
                foreach (var folder in folders)
                {
                    string animeName = InputStringHandler.RemoveUnnecessaryNamePieces(folder.Name);
                    ConsoleExt.WriteLineWithPretext($"Anime Name Extracted: {animeName}", ConsoleExt.OutputType.Info);
                    
                    var animeSearchResults = DbHandler.GetAnimesWithTitle(animeName);
                    
                    if (animeSearchResults == null)
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
                }
            }
            else
            {
                // Gets files and folder information
                FileInfo[] files = directoryInfo.GetFiles();
            
                // displays the name of the folder and number of files
                int fileCount = files.Length;
                ConsoleExt.WriteLineWithPretext($"Folder Name: '{directoryInfo.Name}'", ConsoleExt.OutputType.Info);
                ConsoleExt.WriteLineWithPretext($"Found {fileCount} files in '{path}'", ConsoleExt.OutputType.Info);
            
                // Extract the folder name
                string animeName = InputStringHandler.RemoveUnnecessaryNamePieces(directoryInfo.Name);
                ConsoleExt.WriteLineWithPretext($"Anime Name Extracted: {animeName}", ConsoleExt.OutputType.Info);

                // Search extracted folder name in database
                var animeSearchResults = DbHandler.GetAnimesWithTitle(animeName);

                // if not found write waring message
                if (animeSearchResults == null)
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
    
    // give user options to choose the right anime and import their selected anime into library folder
}