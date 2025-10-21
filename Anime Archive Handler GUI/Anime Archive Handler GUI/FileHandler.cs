using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using FFMpegCore;
using IniParser;

namespace Anime_Archive_Handler_GUI;
using static FileHandler;

public static class FileHandler
{
    internal static readonly string CacheFilePath = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory)!, "Settings/FilePathCache.json");
    private static readonly Dictionary<string, string> FileCache = JsonFileUtility.LoadCache(CacheFilePath);
    private static string? _errorLogFile;
    
    /// <summary>
    /// Checks if the file is valid to move by checking for file existence and integrity.
    /// </summary>
    /// <param name="sourceFile">Source file</param>
    /// <param name="destinationFile">Destination file</param>
    /// <returns>Returns true if the file is valid to move</returns>
    public static bool IsValidToMove(string sourceFile, string destinationFile)
    {
        var existence = CheckForExistence(sourceFile, destinationFile);
        if (!existence) return FileIntegrityCheck([sourceFile]);
        ConsoleExt.WriteLineWithPretext("File Already Exists in Output Folder", ConsoleExt.OutputType.Warning);
        return !FileIntegrityCheck([sourceFile, destinationFile]);
    }
    
    /// <summary>
    /// Checks if the files are valid to move by checking for file existence and integrity.
    /// </summary>
    /// <param name="sourceFiles">List of source files</param>
    /// <param name="destinationFiles">List of destination files</param>
    /// <returns>List of booleans indicating if the files are valid to move</returns>
    public static List<bool> IsValidToMove(List<string> sourceFiles, List<string> destinationFiles)
    {
        List<bool> isValid = new();
        for (var i = 0; i < sourceFiles.Count; i++)
        {
            var existence = CheckForExistence(sourceFiles[i], destinationFiles[i]);
            if (!existence) isValid.Add(FileIntegrityCheck(sourceFiles));
            ConsoleExt.WriteLineWithPretext("File Already Exists in Output Folder", ConsoleExt.OutputType.Warning);
            isValid.Add(!FileIntegrityCheck([sourceFiles[i], destinationFiles[i]]));
        }
        return isValid;
    }
    
    //File integrity checks if all the files in the anime folder aren't corrupted and returns false if the file is corrupt and is used to check if the downloaded anime is fully working
    // and if one of the filed in the anime stored structure is corrupted
    public static bool FileIntegrityCheck(IEnumerable<string> videoFilePaths)
    {
        var episodeNumber = 1;
        var nothingCorrupt = true;

        try
        {
            foreach (var videoFilePath in videoFilePaths)
            {
                if (!File.Exists(videoFilePath))
                {
                    ConsoleExt.WriteLineWithPretext($"File not found: {videoFilePath}", ConsoleExt.OutputType.Error);
                    nothingCorrupt = false;
                    continue;
                }
                
                FFProbe.Analyse(videoFilePath);
                episodeNumber++;
            }
        }
        catch (FileNotFoundException fnfEx)
        {
            ConsoleExt.WriteLineWithPretext($"File not found: {fnfEx.FileName}", ConsoleExt.OutputType.Error);
            nothingCorrupt = false;
        }
        catch (Exception e)
        {
            ConsoleExt.WriteLineWithPretext($"Anime Episode {episodeNumber} encountered an error!", ConsoleExt.OutputType.Error, e);
            ConsoleExt.WriteLineWithPretext(e, ConsoleExt.OutputType.Error);
            nothingCorrupt = false;
        }

        return nothingCorrupt;
    }
    
    public static ObservableCollection<EpisodeDisplayItem> GetAnimeEpisodeList(string animePath) // TODO: Make this funtional
    {
        
        
        return new ObservableCollection<EpisodeDisplayItem>();
    }
    
    /// <summary>
    /// Reads the Animetosho text file and converts it to a CSV file.
    /// </summary>
    /// <param name="filePath">The path to the text file to be converted.</param>
    /// <returns>String containing the path to the new CSV file.</returns>
    public static string ReadAnimetoshoTxt(string filePath)
    {
        try
        {
            var outputFilePath = Path.ChangeExtension(filePath, ".csv"); // Path for the new CSV file

            // Reading from the text file
            using (var reader = new StreamReader(filePath))
            using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       Delimiter = "\t", // Set the delimiter used in your text file to tabs
                       HasHeaderRecord = true, // If your file has header row
                   }))
            {
                // Writing to the CSV file
                using (var writer = new StreamWriter(outputFilePath))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<Animetosho>();
                    csvWriter.WriteRecords(records);
                }
            }

            ConsoleExt.WriteLineWithPretext("File converted successfully.");
            return outputFilePath;
        }
        catch (Exception? e)
        {
            // Log or print exception details
            ConsoleExt.WriteLineWithPretext("Error converting file: ", ConsoleExt.OutputType.Error, e);
            throw;
        }
    }
    
    /// <summary>
    /// Extracts the Audio Track Language by reading the Metadata and is used for language detection of a downloaded anime.
    /// </summary>
    /// <param name="videoFilePath">Path to the video file</param>
    /// <returns>List of Audio Track Languages</returns>
    public static List<string?> TrackLanguageFromMetadata(string videoFilePath)
    {
        var mediaInfo = FFProbe.Analyse(videoFilePath);

        return mediaInfo.AudioStreams.Select(audioStream => audioStream.Language)
            .Where(audioStreamLanguage => audioStreamLanguage != null)
            .Where(audioStreamLanguage => audioStreamLanguage != null).ToList();
    }
    
    /// <summary>
    /// Checks if the file already existing in the output folder using the MD5 checksum.
    /// </summary>
    /// <param name="source">Source File</param>
    /// <param name="destination">Destination File</param>
    /// <returns>Bool is true if the files are the same</returns>
    private static bool CheckForExistence(string source, string destination)
    {
        var sourceHash = GetMd5Checksum(source);
        var destinationHash = GetMd5Checksum(destination);

        return sourceHash == destinationHash;
    }
    
    /// <summary>
    /// Calculates the MD5 checksum of a file.
    /// </summary>
    /// <param name="filePath">Path to the file</param>
    /// <returns>String containing the MD5 checksum</returns>
    private static string GetMd5Checksum(string filePath)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
    
    /// <summary>
    /// Checks if a file exists, and creates it if it doesn't.
    /// </summary>
    /// <param name="fileToCheck">The path to the file to check</param>
    public static void CheckFileExistence(string fileToCheck)
    {
        if (!File.Exists(fileToCheck)) File.Create(fileToCheck);
    }
    
    /// <summary>
    /// Gets the path to a file in the program directory.
    /// </summary>
    /// <param name="fileNameWithExtension">The name of the file with its extension</param>
    /// <returns>Returns the path to the file</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static string GetFileInProgramFolder(string fileNameWithExtension)
    {
        if (FileCache.TryGetValue(fileNameWithExtension, out var cachedPath) && File.Exists(cachedPath))
        {
            return cachedPath;
        }

        var baseDirectory = Path.GetDirectoryName(Environment.CurrentDirectory)!;

        foreach (var file in Directory.GetFiles(baseDirectory, fileNameWithExtension, SearchOption.AllDirectories))
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithExtension);
            FileCache[fileNameWithoutExtension] = file;
            JsonFileUtility.SaveCache(FileCache, CacheFilePath); // Save the cache every time it's updated
            return file;
        }

        var message = $"Couldn't find {fileNameWithExtension} file in program directory!";
        ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new FileNotFoundException());
        throw new FileNotFoundException(message);
    }

    /// <summary>
    /// Gets the path to a directory in the program folder.
    /// </summary>
    /// <param name="directoryName">Name of the Folder to look for</param>
    /// <returns>Path to the directory</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string GetDirectoryInProgramFolder(string directoryName)
    {
        foreach (var directory in Directory.GetDirectories(Path.GetDirectoryName(Environment.CurrentDirectory)!, directoryName, SearchOption.AllDirectories))
        {
            return directory;
        }

        var message = $"Couldn't find {directoryName} directory in program directory!";
        ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new InvalidOperationException());
        throw new InvalidOperationException();
    }
    
    //keeps a running log of all the errors that occured when the program was running and stores them in one file
    //doesn't reuse the same file when the program is restarted
    //has to keep a file directory record of the file when created when the first error occurs, and has to delete that record when the program is closed
    // could use a uid that gets generated new everytime the program gets started, but this uid needs to get associated with the log file
    internal static void ErrorLogger(string errorInfo, Exception? ex)
    {
        if (_errorLogFile == null || File.Exists(_errorLogFile))
        {
            var stream = File.Create(Path.Combine(GetDirectoryInProgramFolder("Errors"), $"Error Log {HelperClass.PathFriendlyDateTime()}.txt"));
            _errorLogFile = stream.Name;
            stream.Close();
        }
        // Log the error or handle it as needed
        var errorMessage = $"Error, {errorInfo}: {ex?.Message}";
        ConsoleExt.WriteLineWithPretext(errorMessage, ConsoleExt.OutputType.Error);

        // Write the error message to the log file
        using var logWriter = new StreamWriter(Path.Combine(GetDirectoryInProgramFolder("Errors"), _errorLogFile), append: true);
        logWriter.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {errorMessage}");
        // Optionally, write more details about the error or the problematic record
    }
}

public static partial class SettingsManager 
{
    private static readonly FileIniDataParser Parser = new();

    /// <summary>
    /// Gets the value of a specified setting from a file.
    /// </summary>
    /// <param name="filePath">Path to the file</param>
    /// <param name="sectionName">Name of the section</param>
    /// <param name="keyName">Name of the key</param>
    /// <returns>String containing the value</returns>
    private static string GetValue(string filePath, string sectionName, string keyName)
    {
        var data = Parser.ReadFile(filePath);
        var match = MyRegex().Match(data[sectionName][keyName]);

        // checks for ./ which means that it's a directory, so it will return a full directory, otherwise returns a unchanged string
        return match.Success ? GetDirectoryInProgramFolder(MyRegex().Replace(data[sectionName][keyName], "")) : data[sectionName][keyName];
    }
    
    /// <summary>
    /// Saves a specified setting to a file.
    /// </summary>
    /// <param name="filePath">Path to the file</param>
    /// <param name="sectionName">Name of the section</param>
    /// <param name="keyName">Name of the key</param>
    /// <param name="value">Value to save</param>
    public static void SaveSetting(string filePath, string sectionName, string keyName, string value)
    {
        string userSettings = JsonFileUtility.LoadCache(CacheFilePath)["UserSettings"];
        var data = Parser.ReadFile(userSettings);
        data[sectionName][keyName] = value;
        Parser.WriteFile(filePath, data);
    }
    
    /// <summary>
    /// Gets the path of a file or folder from the cache or searches for it in the specified root path if it isn't cached.
    /// </summary>
    /// <param name="folderOrFileName">The name of the file or folder</param>
    /// <param name="rootPath">The root path to search for the file or folder</param>
    /// <returns>String containing the path</returns>
    public static string GetPathSetting(string folderOrFileName, string rootPath)
    {
        if (JsonFileUtility.LoadCache(CacheFilePath).TryGetValue(folderOrFileName, out var cachedPath) && Path.Exists(cachedPath)) // checks if the input folderOrFileName is cached
        {
            return cachedPath;
        }
        if (Path.GetExtension(folderOrFileName) == "") // checks if the input folderOrFileName is a file
        {
            string[] directories = Directory.GetDirectories(rootPath, folderOrFileName, SearchOption.AllDirectories); // need to cache the path of the file in the file cache
            if (directories.Length > 0)
            {
                return directories[0];
            }
        }
        else // if the input folderOrFileName is a folder
        {
            string[] files = Directory.GetFiles(rootPath, folderOrFileName, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                return files[0];
            }
        }

        return String.Empty; // returns an empty string if the file or folder isn't found
    }
    
    /// <summary>
    /// Gets a specified setting from either the user settings or the default settings.
    /// </summary>
    /// <param name="sectionName">Name of the section</param>
    /// <param name="keyName">Name of the key</param>
    /// <returns>Returns a string containing the value</returns>
    public static string GetSetting(string sectionName, string keyName)
    {
        string settings = CommonSettings.SettingsPath;
        string userSettings = GetFileInProgramFolder("UserSettings.ini");

        // checks if the user has set a value in that settings section and caches and returns it if they did
        string setting = GetValue(userSettings, sectionName, keyName);
        if (setting != "null")
        {
            return setting;
        }
        
        // checks if there is a default value for that setting and caches and returns it
        setting = GetValue(settings, $"Default {sectionName}", keyName);
        return setting;
    }

    internal static string GoGetter()
    {
        string settings = GetFileInProgramFolder("Settings.ini");
        GetFileInProgramFolder("UserSettings.ini");
        return settings;
    }

    [GeneratedRegex(@"\./")]
    private static partial Regex MyRegex();
}