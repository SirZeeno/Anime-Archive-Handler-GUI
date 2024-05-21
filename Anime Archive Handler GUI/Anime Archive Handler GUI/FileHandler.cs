using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
    internal static readonly string CacheFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Settings/Cache.json");
    private static readonly Dictionary<string, string> FileCache = JsonFileUtility.LoadCache(CacheFilePath);
    private static string? _errorLogFile;
    
    // Implements checks for file existence, integrity, etc.
    public static bool IsValidToMove(string sourceFile, string destinationFile)
    {
        var existence = CheckForExistence(sourceFile, destinationFile);
        if (!existence) return FileIntegrityCheck([sourceFile]);
        ConsoleExt.WriteLineWithPretext("File Already Exists in Output Folder", ConsoleExt.OutputType.Warning);
        return !FileIntegrityCheck([sourceFile, destinationFile]);
    }
    
    //File integrity checks if all the files in a anime folder aren't corrupted and returns false if the file is corrupt and is used to check if the downloaded anime is fully working
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
            ConsoleExt.WriteLineWithPretext($"Anime Episode {episodeNumber} encountered an error!", ConsoleExt.OutputType.Error);
            ConsoleExt.WriteLineWithPretext(e, ConsoleExt.OutputType.Error);
            nothingCorrupt = false;
        }

        return nothingCorrupt;
    }
    
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

            ConsoleExt.WriteLineWithPretext("File converted successfully.", ConsoleExt.OutputType.Info);
            return outputFilePath;
        }
        catch (Exception? e)
        {
            // Log or print exception details
            ConsoleExt.WriteLineWithPretext("Error converting file: ", ConsoleExt.OutputType.Error, e);
            throw;
        }
    }
    
    //Extracts the Audio Track Language by reading the Metadata and is used for language detection of a downloaded anime
    public static List<string?> TrackLanguageFromMetadata(string videoFilePath)
    {
        var mediaInfo = FFProbe.Analyse(videoFilePath);

        return mediaInfo.AudioStreams.Select(audioStream => audioStream.Language)
            .Where(audioStreamLanguage => audioStreamLanguage != null)
            .Where(audioStreamLanguage => audioStreamLanguage != null).ToList();
    }
    
    // Checks if the file already existing in the output folder
    private static bool CheckForExistence(string source, string destination)
    {
        var sourceHash = GetMd5Checksum(source);
        var destinationHash = GetMd5Checksum(destination);

        return sourceHash == destinationHash;
    }
    
    // Calculate MD5 checksum of a file and is used for checking if two of the same files are actually the same
    private static string GetMd5Checksum(string filePath)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
    
    // Checks the existence of a file, creates it if it doesn't exist
    public static void CheckFileExistence(string fileToCheck)
    {
        if (!File.Exists(fileToCheck)) File.Create(fileToCheck);
    }
    
    // need to start caching all those, so it only has to read them from the cache and check if the exist and if they don't, search for them again
    // returns the file path in the program folder and is used to find a file in the program directory when it isn't always going to be in the same place
    public static string GetFileInProgramFolder(string fileNameWithExtension)
    {
        if (FileCache.TryGetValue(fileNameWithExtension, out var cachedPath) && File.Exists(cachedPath))
        {
            return cachedPath;
        }

        var baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        foreach (var file in Directory.GetFiles(baseDirectory, fileNameWithExtension, SearchOption.AllDirectories))
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithExtension);
            FileCache[fileNameWithoutExtension] = file;
            JsonFileUtility.SaveCache(FileCache, CacheFilePath); // Save the cache every time it's updated
            return file;
        }

        var message = $"Couldn't find {fileNameWithExtension} file in program directory!";
        ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new FileNotFoundException());
        throw new FileNotFoundException($"");
    }

    // returns the directory in the program folder and is used when the folder directory that i'm looking for isn't always in the same spot
    public static string GetDirectoryInProgramFolder(string directoryName)
    {
        foreach (var directory in Directory.GetDirectories(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, directoryName, SearchOption.AllDirectories))
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

    // Returns a specified setting which can be used to get user settings or stored settings
    private static string GetValue(string filePath, string sectionName, string keyName)
    {
        var data = Parser.ReadFile(filePath);
        var match = MyRegex().Match(data[sectionName][keyName]);

        // checks for ./ which means that it's a directory, so it will return a full directory, otherwise returns a unchanged string
        return match.Success ? GetDirectoryInProgramFolder(MyRegex().Replace(data[sectionName][keyName], "")) : data[sectionName][keyName];
    }
    
    // have to rework this, so it works with the cache json file instead of th ini file to cache things, and use the settings.ini for default values and error logging
    public static string GetSetting(string sectionName, string keyName)
    {
        string settings = CommonSettings.SettingsPath;
        string userSettings = JsonFileUtility.LoadCache(CacheFilePath)["UserSettings"];

        // checks if the user has set a value in that settings and caches and returns it if they did
        string setting = GetValue(userSettings, sectionName, keyName);
        if (setting != "null")
        {
            return setting;
        }

        try
        {
            // checks if there is a cached value for that setting and returns it
            setting = JsonFileUtility.LoadCache(CacheFilePath)[keyName];
            if (setting != "null") return setting;
        }
        catch
        {
            // ignored
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