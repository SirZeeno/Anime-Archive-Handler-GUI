using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Anime_Archive_Handler_GUI;

public static class JsonFileUtility
{
    /// <summary>
    /// Gets the languages supported and selected from the JSON file
    /// </summary>
    /// <param name="filePath">Path to the JSON file</param>
    /// <returns>List of supported and selected languages</returns>
    public static List<Languages>? GetLanguages(string filePath)
    {
        // Read the file content into a string
        string json = File.ReadAllText(filePath);
        
        // Deserialize from JSON to Language structure
        return JsonConvert.DeserializeObject<List<Languages>>(json);
    }

    /// <summary>
    /// Writes the supported and selected languages to the JSON file
    /// </summary>
    /// <param name="filePath">Path to the JSON file</param>
    /// <param name="root">List of supported and selected languages</param>
    public static void WriteLanguages(string filePath, List<Languages> root)
    {
        // Serialize back to JSON
        var updatedJsonText = JsonConvert.SerializeObject(root, Formatting.Indented);
        File.WriteAllText(filePath, updatedJsonText);
    }
    
    /// <summary>
    /// Reads the necessary directories for the Program to work from the JSON file
    /// </summary>
    /// <param name="filePath">Path to the JSON file</param>
    /// <returns>List of necessary directories</returns>
    public static List<NeededDirectories> ReadNeededDirectories(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var neededDirectories = JsonConvert.DeserializeObject<List<NeededDirectories>>(json);

        return neededDirectories ?? [];
    }
    
    /// <summary>
    /// Gets the list of verbs from the JSON file
    /// </summary>
    /// <param name="filePath">Path to the JSON file</param>
    /// <returns>List of verbs</returns>
    public static List<string> GetVerbsList(string filePath) // Todo: There is no need to ask for the file path when i can get the path using the assembly or my location function
    {
        var json = File.ReadAllText(filePath);
        var verbList = JsonSerializer.Deserialize<VerbList>(json);
        return verbList?.verbs ?? new List<string>();
    }

    public static NHentaiMetaData? ReadMetaData(string filePath)
    {
        NHentaiMetaData? metaData = JsonConvert.DeserializeObject<NHentaiMetaData>(filePath);
        return metaData;
    }
    
    /// <summary>
    /// Saves the cache to the specified file path. The cache is a Dictionary where the key is the file name, and the value is the file path.
    /// </summary>
    /// <param name="fileCache">Dictionary containing the cache</param>
    /// <param name="cacheFilePath">Path to the cache file</param>
    public static void SaveCache(Dictionary<string, string> fileCache, string cacheFilePath)
    {
        var json = JsonSerializer.Serialize(fileCache);
        File.WriteAllText(cacheFilePath, json);
    }

    /// <summary>
    /// Loads the cache from the specified file path.
    /// If the file does not exist or the cache is null, it returns an empty Dictionary.
    /// </summary>
    /// <param name="cacheFilePath">Path to the cache file</param>
    /// <returns>String Dictionary containing the cache</returns>
    public static Dictionary<string, string> LoadCache(string cacheFilePath)
    {
        var fileCache = new Dictionary<string, string>();
        if (!File.Exists(cacheFilePath)) return new Dictionary<string, string>();
        var json = File.ReadAllText(cacheFilePath);
        var cache = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if (cache == null) return new Dictionary<string, string>();
        foreach (var kvp in cache)
        {
            fileCache[kvp.Key] = kvp.Value;
        }

        return fileCache;
    }
}