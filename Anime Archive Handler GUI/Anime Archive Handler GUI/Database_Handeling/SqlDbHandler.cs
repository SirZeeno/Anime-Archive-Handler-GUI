using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Anime_Archive_Handler_GUI.Database_Handeling;

public static class SqlDbHandler
{
    // Gets all anime titles of a specific anime by id
    public static ICollection<TitleEntryDto> GetAnimeTitlesById(long? malId)
    {
        using var sqliteContext = new AnimeContext();
        var anime = sqliteContext.Animes.Find(malId);
        sqliteContext.Entry(anime).Collection("Titles").Load();
        return anime.Titles;
    }
    
    public static ImagesSetDto GetAnimeImagesById(long? malId)
    {
        using var sqliteContext = new AnimeContext();
        var anime = sqliteContext.Animes.Include(a => a.Images)
            .ThenInclude(i => i.JPG)
            .Include(a => a.Images)
            .ThenInclude(i => i.WebP)
            .FirstOrDefault(a => a.MalId == malId);
        return anime.Images;
    }
    
    public static Dictionary<long?, ImagesSetDto> GetAnimeImagesByIds(List<long?> malIds)
    {
        using var sqliteContext = new AnimeContext();
    
        var animes = sqliteContext.Animes
            .Include(a => a.Images)
            .ThenInclude(i => i.JPG)
            .Include(a => a.Images)
            .ThenInclude(i => i.WebP)
            .Where(a => malIds.Contains(a.MalId))
            .ToList();
    
        return animes.ToDictionary(a => a.MalId, a => a.Images);
    }
    
    public static Dictionary<long?, ICollection<TitleEntryDto>> GetAnimeTitlesByIds(List<long?> malIds)
    {
        using var sqliteContext = new AnimeContext();
    
        var animes = sqliteContext.Animes
            .Include(a => a.Titles)
            .Where(a => malIds.Contains(a.MalId))
            .ToList();
    
        return animes.ToDictionary(a => a.MalId, a => a.Titles);
    }
    
    public static Dictionary<long?, AnimeDto> GetAnimesByIds(List<long?> malIds)
    {
        using var sqliteContext = new AnimeContext();
        return sqliteContext.Animes.Where(a => malIds.Contains(a.MalId)).ToDictionary(a => a.MalId, a => a);
    }
    
    // Gets a specific anime by id
    public static AnimeDto? GetAnimeById(long malId)
    {
        using var sqliteContext = new AnimeContext();
        return sqliteContext.Animes.Find(malId);
    }
    
    // Get all anime similar titles by title
    public static List<TitleEntryDto> GetAnimeTitlesByTitle(string animeTitle)
    {
        using var sqliteContext = new AnimeContext();
        sqliteContext.Database.EnsureCreated();
        var anime = sqliteContext.TitleEntries.Where(a => EF.Functions.Like(a.Title, $"%{animeTitle}%") || a.Title.ToLower() == animeTitle.ToLower()).ToList();
        return anime;
    }  
    
    // Updates the fts table from the title entries
    public static void UpdateTitleFts()
    {
        using var sqliteContext = new AnimeContext();
        sqliteContext.Database.EnsureCreated();
        sqliteContext.TitlesFts.FromSqlRaw("drop table Titles_fts;");
        sqliteContext.TitlesFts.FromSqlRaw("CREATE VIRTUAL TABLE Titles_fts USING fts5(AnimeId UNINDEXED, Title, Type UNINDEXED);");
        sqliteContext.TitlesFts.FromSqlRaw("INSERT INTO Titles_fts (AnimeId, Title, Type) SELECT AnimeId, Title, Type FROM TitleEntries;");
    }
    
    public static List<TitleFtsDto> SearchTitles(string searchText) // runs through different methods to search for the title
    {
        using var sqliteContext = new AnimeContext();
        List<TitleFtsDto> matchingTitles = new List<TitleFtsDto>();
        string safeSearchText = searchText.Replace("'", "\"\"");
        if (safeSearchText.Contains(",") || safeSearchText.Contains("-") || safeSearchText.Contains(".")) safeSearchText = "\"" + safeSearchText + "\"";
        
        // Conventional Search
        var conventionalSearch = sqliteContext.TitlesFts.FromSqlRaw($"SELECT * FROM Titles_fts WHERE Title MATCH '{safeSearchText}';").ToList();
        if (conventionalSearch.Count > 0) return conventionalSearch;
        
        // Filtered Split Search
        var splitTextver1 = safeSearchText.Split(" ");
        var exclusionWords = JsonFileUtility.GetVerbsList(FileHandler.GetFileInProgramFolder("ExclusionWords.json"));
        
        foreach (var split in splitTextver1)
        {
            if (exclusionWords.Where(x => x == split.Trim().ToLower()).Any()) continue;
            var anime = sqliteContext.TitlesFts.FromSqlRaw($"SELECT * FROM Titles_fts WHERE Title MATCH '{split}*';").ToList();
            matchingTitles.AddRange(anime);
        }
        if (matchingTitles.Count > 0) return matchingTitles;
        
        
        // Split Search
        var splitTextver2 = safeSearchText.SplitIntoChunks(int.Parse(SettingsManager.GetSetting("Execution Settings", "StringSplitRange")));
        foreach (var split in splitTextver2)
        {
            ConsoleExt.WriteLineWithPretext(split, ConsoleExt.OutputType.Info);
            var anime = sqliteContext.TitlesFts.FromSqlRaw($"SELECT * FROM Titles_fts WHERE Title MATCH '{split}*';").ToList(); // if it's the last string in the split, it will not add * at the end or add * before the last string
            matchingTitles.AddRange(anime);
        }
        return matchingTitles;
    }
    
    public static List<AnimeDto> GetAnimeByTitle(string animeTitle)
    {
        using var sqliteContext = new AnimeContext();
        var titles = SearchTitles(animeTitle);
        List<long?> malIds = new List<long?>();
        foreach (var title in titles)
        {
            malIds.Add(title.AnimeId);
        }
        return GetAnimesByIds(malIds).Values.ToList();
    }
}