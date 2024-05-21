using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Interfaces;
using FuzzySharp;
using FuzzySharp.Extractor;
using LiteDB;

namespace Anime_Archive_Handler_GUI;
using static CommonSettings;

public static class DbHandler
{
    public static void EnsureIndexDb()
    {
        // Ensure index on MalId
        AnimeDb.EnsureIndex(x => x.MalId);
        TitleEntryListDb.EnsureIndex(x => x.MalId);
        ToWatchListDb.EnsureIndex(x => x.MalId);
        ToWatchListTitlesDb.EnsureIndex(x => x.MalId);
        
        // Ensure index on Title
        TitleEntryListDb.EnsureIndex(x => x.Title);
        ToWatchListTitlesDb.EnsureIndex(x => x.Title);
    }

    public static void PopulateTitleEntryDb()
    {
        var animes = AnimeDb.FindAll();
        foreach (var anime in animes)
        {
            foreach (var titleEntry in anime.Titles)
            {
                var titleEntryDb = new TitleEntryDb()
                {
                    MalId = anime.MalId,
                    Title = titleEntry.Title,
                    Type = titleEntry.Type
                };
                TitleEntryListDb.Upsert(titleEntryDb);
            }
        }
    }

    public static async Task SaveToDb<T>(T inputPath, IEditingDatabase editingDatabase)
    {
        await editingDatabase.AddToDatabase(inputPath);
    }
    
    public static async void RemoveFromDb(long id, IEditingDatabase editingDatabase)
    {
        await editingDatabase.RemoveFromDatabase(id);
    }

    public static IEditingDatabase DetermineEditingClass<T>(T input)
    {
        string? inputPath = input?.ToString();
        if (Path.HasExtension(inputPath) && Path.GetExtension(inputPath) == ".csv")
        {
            return new EditAnimetoshoDb();
        }

        if (input is AnimeDto)
        {
            return new EditAnimeList();
        }

        return new EditNhentaiDb();
    }

    public static bool CheckAnimeExistence(long? malId)
    {
        var anime = ToWatchListDb.FindOne(x => x != null && x.MalId == malId);
        var animeTitle = ToWatchListTitlesDb.FindOne(x => x != null && x.MalId == malId);
        return anime != null || animeTitle != null;
    }

    public static object? FindById<T>(long? malId, ILiteCollection<T> database)
    {
        if (database == AnimeDb)
        {
            var d = database as ILiteCollection<AnimeDto?>;
            return d?.FindOne(x => x != null && x.MalId == malId);
        }

        if (database != ToWatchListTitlesDb) return null;
        {
            var d = database as ILiteCollection<TitleEntryDb?>;
            return d?.FindOne(x => x != null && x.MalId == malId);
        }
    }

    public static long? FindLastAnimeIdInDb()
    {
        return AnimeDb.FindOne(Query.All("MalId", Query.Descending)).MalId;
    }
    
    /*
    public static AnimeDto? GetAnimeWithTitle(string title)
    {
        var similarityPercentage = int.Parse(SettingsManager.GetSetting("Execution Settings", "SimilarityPercentage"));
        var normalizedTitle = NormalizeTitle(title);

        // Fetch potential matches from the database 
        var potentialMatches = FetchPotentialMatchesFromDatabase(normalizedTitle);

        var enumerable = potentialMatches.ToList();

        // Use Process.ExtractTop() to get the best match
        var matches = Process.ExtractTop(normalizedTitle, enumerable);

        var extractedResults = matches as ExtractedResult<string>[] ?? matches.ToArray();
        if (extractedResults.Length == 0 || extractedResults.First().Score <= similarityPercentage) return null;
        
        // Use LiteDB's Query syntax to find the first matching record based on the title
        var titleEntryDb = TitleEntryListDb.Find(Query.EQ("Title", extractedResults.First().Value)).FirstOrDefault();

        if (titleEntryDb == null) return null;
        var malId = titleEntryDb.MalId;
        return AnimeDb.FindOne(Query.EQ("MalId", malId));
    }
    */
    
    public static IEnumerable<AnimeDto>? GetAnimesWithTitle(string title)
    {
        var similarityPercentage = int.Parse(SettingsManager.GetSetting("Execution Settings", "SimilarityPercentage"));
        var normalizedTitle = NormalizeTitle(title);

        // Fetch potential matches from the database 
        var potentialMatches = FetchPotentialMatchesFromDatabase(normalizedTitle);

        var enumerable = potentialMatches.ToList();

        // Use Process.ExtractTop() to get the best match
        var matches = Process.ExtractTop(normalizedTitle, enumerable);

        var extractedResults = matches as ExtractedResult<string>[] ?? matches.ToArray();
        if (extractedResults.Length == 0 || extractedResults.First().Score <= similarityPercentage) return null;
        
        // Use LiteDB's Query syntax to find the first matching record based on the title
        var titleEntryDb = TitleEntryListDb.Find(Query.EQ("Title", extractedResults.First().Value));

        if (titleEntryDb == null) return null;
        var malId = titleEntryDb.First().MalId;
        return AnimeDb.Find(Query.EQ("MalId", malId));
    }

    private static string NormalizeTitle(string title)
    {
        return title.ToLower().Trim();
    }
    
    private static IEnumerable<string> FetchPotentialMatchesFromDatabase(string normalizedTitle)
    {
        var potentialTitles = new HashSet<string>();
        var characterSearchRange = int.Parse(SettingsManager.GetSetting("Execution Settings", "CharacterSearchRange"));
    
        // Fetch all potential TitleEntryDb from the database
        var allTitleEntries = TitleEntryListDb.FindAll().ToHashSet();

        // Filter titles based on the first number of characters
        foreach (var titleEntry in from titleEntry in allTitleEntries where titleEntry.Title != null let firstNCharacters = titleEntry.Title[..Math.Min(characterSearchRange, titleEntry.Title.Length)] where firstNCharacters.ToCharArray().Any(normalizedTitle.Contains) select titleEntry)
        {
            potentialTitles.Add(titleEntry.Title);
        }

        return potentialTitles;
    }
        
    public static string GetAnimeTitleWithAnime(AnimeDto? anime)
    {
        string? englishTitle = null;
        string? defaultTitle = null;

        if (anime != null)
            foreach (var title in anime.Titles)
                switch (title.Type.ToLower())
                {
                    case "english":
                        englishTitle = title.Title;
                        break;
                    //default is mostly japanese in English characters
                    case "default":
                        defaultTitle = title.Title;
                        break;
                }

        if (englishTitle != null) return englishTitle;

        return defaultTitle ?? "";
    }
}