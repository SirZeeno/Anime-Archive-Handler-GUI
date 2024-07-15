using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Interfaces;
using FuzzySharp;
using LiteDB;

namespace Anime_Archive_Handler_GUI.Database_Handeling;
using static CommonSettings;

public static class LiteDbHandler
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
    
    // TODO: Add that animes that haven't been released yet dont get returned unless specified
    // TODO: Rewrite this method to use less task runs
    /*
    public static async Task<IEnumerable<AnimeDto>?> GetAnimesWithTitle(string title)
    {
        var similarityPercentage = int.Parse(SettingsManager.GetSetting("Execution Settings", "SimilarityPercentage"));
        var similarityDistance = int.Parse(SettingsManager.GetSetting("Execution Settings", "SimilarityDistance"));
        var normalizedTitle = NormalizeTitle(title);

        // Fetch potential matches from the database asynchronously
        var potentialMatches = await FetchPotentialMatchesFromDatabaseAsync(normalizedTitle);

        var titleEntryDbs = potentialMatches as TitleEntryDb[] ?? potentialMatches.ToArray();
        var titles = titleEntryDbs.Select(x => x.Title).ToList(); // Why does it return nothing if i use ToLower() here?
    
        // Use RankNames to get the best matches
        var matches = RankNames(normalizedTitle, titles);
        var extractedMatches = matches.Where(match => match.Item2 <= similarityDistance)
            .Select(match => match.Item1)
            .ToList();
    
        var fuzzyMatches = Process.ExtractTop(normalizedTitle, extractedMatches, cutoff: similarityPercentage, limit: 10).ToArray();

        var animeDtos = new List<AnimeDto>();
    
        foreach (var match in fuzzyMatches)
        {
            // Concurrently perform database queries
            var titleEntryDb = titleEntryDbs.FirstOrDefault(x => x.Title == match.Value);

            if (titleEntryDb == null) continue;
        
            var malId = titleEntryDb.MalId;
            var animeDbTask = Task.Run(() => AnimeDb.Find(Query.EQ("MalId", malId)).ToList());

            var animeDb = await animeDbTask;
            if (animeDb.Any())
            {
                animeDtos.Add(animeDb.First());
            }
        }
    
        return animeDtos;
    }
    */

    private static List<Tuple<string, int>> RankNames(string targetName, List<string> nameList)
    {
        List<Tuple<string, int>> rankedList = new List<Tuple<string, int>>();

        foreach (var name in nameList)
        {
            int distance = HelperClass.LevenshteinDistance(targetName, name);
            rankedList.Add(new Tuple<string, int>(name, distance));
        }

        return rankedList.OrderBy(t => t.Item2).ToList();
    }

    private static string NormalizeTitle(string title)
    {
        return title.ToLower().Trim();
    }
    
    private static async Task<IEnumerable<TitleEntryDb>> FetchPotentialMatchesFromDatabaseAsync(string normalizedTitle)
    {
        var potentialTitles = new HashSet<TitleEntryDb>();
        var characterSearchRange = int.Parse(SettingsManager.GetSetting("Execution Settings", "CharacterSearchRange"));

        // Fetch and filter TitleEntryDb from the database asynchronously
        var filteredTitleEntries = await Task.Run(() =>TitleEntryListDb.Find(entry => entry.Title != null && entry.Title.Length >= characterSearchRange));

        foreach (var titleEntry in filteredTitleEntries)
        {
            if (MatchesFirstNCharacters(titleEntry.Title, normalizedTitle, characterSearchRange))
            {
                potentialTitles.Add(titleEntry);
            }
        }

        return potentialTitles;
    }

    private static bool MatchesFirstNCharacters(string title, string normalizedTitle, int characterSearchRange)
    {
        int lengthToCheck = Math.Min(characterSearchRange, title.Length);
        for (int i = 0; i < lengthToCheck; i++)
        {
            if (normalizedTitle.Contains(title[i]))
            {
                return true;
            }
        }
        return false;
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