using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Anime_Archive_Handler_GUI;

public static class JsonFileUtility
{
    public static List<Languages>? GetLanguages(string filePath)
    {
        // Read the file content into a string
        string json = File.ReadAllText(filePath);
        
        // Deserialize from JSON to Language structure
        return JsonConvert.DeserializeObject<List<Languages>>(json);
    }

    public static void WriteLanguages(string filePath, List<Languages> root)
    {
        // Serialize back to JSON
        var updatedJsonText = JsonConvert.SerializeObject(root, Formatting.Indented);
        File.WriteAllText(filePath, updatedJsonText);
    }
    
    public static List<NeededDirectories> ReadNeededDirectories(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var neededDirectories = JsonConvert.DeserializeObject<List<NeededDirectories>>(json);

        return neededDirectories ?? [];
    }

    public static NHentaiMetaData? ReadMetaData(string filePath)
    {
        NHentaiMetaData? metaData = JsonConvert.DeserializeObject<NHentaiMetaData>(filePath);
        return metaData;
    }
    
    public static void SaveCache(Dictionary<string, string> fileCache, string cacheFilePath)
    {
        var json = JsonSerializer.Serialize(fileCache);
        File.WriteAllText(cacheFilePath, json);
    }

    // LoadCache function loads a cache from the specified file path and returns a Dictionary<string, string> containing the cache data. If the specified file does not exist or the cache is null, it returns an empty Dictionary<string, string>.
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