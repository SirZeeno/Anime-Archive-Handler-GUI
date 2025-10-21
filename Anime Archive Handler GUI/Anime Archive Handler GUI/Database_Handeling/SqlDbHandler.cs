using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Anime_Archive_Handler_GUI.Database_Handeling;

public static class SqlDbHandler
{
    public static readonly AnimeContext Context = new AnimeContext();
    
    /// <summary>
    /// Gets all anime titles of specific anime by id
    /// </summary>
    /// <param name="malId">Myanimelist id</param>
    /// <returns>Collection of title entries</returns>
    public static ICollection<TitleEntryDto>? GetAnimeTitlesById(long malId)
    {
        using var sqliteContext = new AnimeContext();
        var anime = sqliteContext.Animes.Find(malId);
        sqliteContext.Entry(anime).Collection("Titles").Load();
        return anime?.Titles;
    }
    
    /// <summary>
    /// Gets all anime images of specific anime by id
    /// </summary>
    /// <param name="malId">Myanimelist id</param>
    /// <returns>Images set</returns>
    public static ImagesSetDto? GetAnimeImagesById(long malId)
    {
        using var sqliteContext = new AnimeContext();
        var anime = sqliteContext.Animes.Include(a => a.Images)
            .ThenInclude(i => i!.JPG)
            .Include(a => a.Images)
            .ThenInclude(i => i!.WebP)
            .FirstOrDefault(a => a.MalId == malId);
        return anime?.Images;
    }

    /// <summary>
    /// Gets all anime images for a list of anime by id async
    /// </summary>
    /// <param name="malIds">List of myanimelist ids</param>
    /// <returns>Task of a Dictionary with Myanimelist ids as keys and images sets as values</returns>
    private static async Task<Dictionary<long, ImagesSetDto>> GetAnimeImagesByIds(List<long> malIds)
    {
        await using var sqliteContext = new AnimeContext();

        var animes = new List<AnimeDto>();
        await Task.Factory.StartNew(() =>
        {
            animes = sqliteContext.Animes
                .Include(a => a.Images)
                .ThenInclude(i => i!.JPG)
                .Include(a => a.Images)
                .ThenInclude(i => i!.WebP)
                .Where(a => malIds.Contains((long)a.MalId!))
                .ToList();
        });
        return animes.ToDictionary(a => (long)a.MalId!, a => a.Images)!;
    }
    
    /// <summary>
    /// Gets all anime bitmap images for a list of anime by id async
    /// </summary>
    /// <param name="malIds">List of myanimelist ids</param>
    /// <returns>Task of a Dictionary with Myanimelist ids as keys and bitmap images sets as values</returns>
    public static async Task<Dictionary<long, AnimeImageSetBitmap>> GetAnimeBitmapImagesByIds(List<long?> malIds)
    {
        await using var sqliteContext = new AnimeContext();
        var animes = new List<AnimeImageSetBitmap>();

        await Task.Factory.StartNew(() =>
        {
            animes = sqliteContext.ImageBitmaps
                .Include(a => a.JPG)
                .Include(a => a.WebP)
                .Where(a => malIds.Contains(a.MalId)).ToList();
        });
        return animes.ToDictionary(a => (long)a.MalId!, a => a);
    }
    
    /// <summary>
    /// Gets all anime titles for a list of anime by id
    /// </summary>
    /// <param name="malIds">List of Myanimelist ids</param>
    /// <returns>Dictionary with Myanimelist ids as keys and title entries as values</returns>
    public static Dictionary<long, ICollection<TitleEntryDto>> GetAnimeTitlesByIds(List<long> malIds)
    {
        using var sqliteContext = new AnimeContext();
    
        var animes = sqliteContext.Animes
            .Include(a => a.Titles)
            .Where(a => malIds.Contains((long)a.MalId!))
            .ToList();
    
        return animes.ToDictionary(a => (long)a.MalId!, a => a.Titles)!;
    }

    /// <summary>
    /// Gets all animes by a list of Myanimelist ids
    /// </summary>
    /// <param name="malIds">List of Myanimelist ids</param>
    /// <returns>Dictionary with Myanimelist ids as keys and animes as values</returns>
    private static Dictionary<long, AnimeDto> GetAnimesByIds(List<long> malIds)
    {
        return Context.Animes.Where(a => malIds.Contains((long)a.MalId!))
            .Include(a => a.ImageBitmaps)
            .ThenInclude(i => i!.JPG)
            .Include(a => a.ImageBitmaps)
            .ThenInclude(i => i!.WebP)
            .Include(a => a.Titles)
            .Include(x => x.Genres)
            .Include(x => x.ExplicitGenres)
            .Include(x => x.Producers)
            .Include(x => x.Licensors)
            .Include(x => x.Studios)
            .Include(x => x.Trailer)
            .ToDictionary(a => (long)a.MalId!, a => a);
    }
    
    /// <summary>
    /// Gets Anime by id
    /// </summary>
    /// <param name="malId">Myanimelist id</param>
    /// <returns>Anime</returns>
    public static AnimeDto? GetAnimeById(long malId)
    {
        return Context.Animes.Find(malId);
    }

    /// <summary>
    /// Processes all images in the database into bitmaps
    /// </summary>
    /// <param name="ids">List of Myanimelist ids</param>
    public static async void ProcessAllImages(List<long>? ids = null)
    {
        try
        {
            int animeCount = (int)Context.Animes.Max(a => a.MalId)!;
            List<long> malIds = ids ?? Enumerable.Range(1, animeCount).ToList().ConvertAll(i => (long) i);
            var images = await Task.Run(() => GetAnimeImagesByIds(malIds).GetAwaiter().GetResult());
            ConsoleExt.WriteLineWithPretext("Got all images");
            List<AnimeImageSetBitmap> animeImageSetBitmaps = new List<AnimeImageSetBitmap>();
            foreach (var image in images)
            {
                ConsoleExt.WriteLineWithPretext("Processing images for malId: " + image.Key);
            
                // JPG
                var jpg = image.Value.JPG;
                var webp = image.Value.WebP;
                var jpgAnimeImageBitmap = new AnimeImageBitmap
                {
                    ImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(jpg.ImageUrl)).GetAwaiter().GetResult(),
                    SmallImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(jpg.SmallImageUrl)).GetAwaiter().GetResult(),
                    MediumImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(jpg.MediumImageUrl)).GetAwaiter().GetResult(),
                    LargeImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(jpg.LargeImageUrl)).GetAwaiter().GetResult(),
                    MaximumImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(jpg.MaximumImageUrl)).GetAwaiter().GetResult()
                };
                var webpAnimeImageBitmap = new AnimeImageBitmap
                {
                    ImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(webp.ImageUrl)).GetAwaiter().GetResult(),
                    SmallImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(webp.SmallImageUrl)).GetAwaiter().GetResult(),
                    MediumImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(webp.MediumImageUrl)).GetAwaiter().GetResult(),
                    LargeImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(webp.LargeImageUrl)).GetAwaiter().GetResult(),
                    MaximumImageBitmap = Task.Run(() => ImageHelper.LoadBytesFromWebTask(webp.MaximumImageUrl)).GetAwaiter().GetResult()
                };
                animeImageSetBitmaps.Add(new AnimeImageSetBitmap
                {
                    MalId = image.Key, 
                    JPG = jpgAnimeImageBitmap, 
                    WebP = webpAnimeImageBitmap
                });
            }
            Context.ImageBitmaps.AddRange(animeImageSetBitmaps);
            await Context.SaveChangesAsync();
            CorrectAnimeImageRelations(ids);
            ConsoleExt.WriteLineWithPretext("Processed all images");
        }
        catch (Exception e)
        {
            ConsoleExt.WriteLineWithPretext($"An error has occured while processing images: {e}", ConsoleExt.OutputType.Error, e);
        }
    }

    /// <summary>
    /// Corrects the relations between anime and images in the database
    /// </summary>
    /// <param name="ids">List of Myanimelist ids</param>
    private static async void CorrectAnimeImageRelations(List<long>? ids = null)
    {
        try
        {
            int animeCount = (int)Context.Animes.Max(a => a.MalId)!;
            List<long> malIds = ids ?? Enumerable.Range(1, animeCount).ToList().ConvertAll(i => (long) i);
            var animes = GetAnimesByIds(malIds);
            foreach (var anime in animes.Values)
            {
                long malId = (long)anime.MalId!;
                var images = await GetAnimeBitmapImagesByIds([malId]);
                anime.ImageBitmaps = images[malId];
                anime.AnimeImageSetBitmapId = images[malId].MalId;
            }
            ConsoleExt.WriteLineWithPretext("Done correcting anime image relations");
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            ConsoleExt.WriteLineWithPretext("An error has occured while correcting anime image relations", ConsoleExt.OutputType.Error, e);
        }
    }
    
    /// <summary>
    /// Gets all animes from the database within a specific count
    /// </summary>
    /// <param name="count">Count</param>
    /// <returns>Queryable of animes</returns>
    public static IQueryable<AnimeDto> GetAnimesByCount(int count)
    {
        return Context.Animes.Take(count);
    }
    
    /// <summary>
    /// Updates the fts table from the title entries
    /// </summary>
    public static void UpdateTitleFts()
    {
        Context.Database.EnsureCreated();
        Context.TitlesFts.FromSqlRaw("drop table Titles_fts;");
        Context.TitlesFts.FromSqlRaw("CREATE VIRTUAL TABLE Titles_fts USING fts5(AnimeId UNINDEXED, Title, Type UNINDEXED);");
        Task.Run(() => Context.TitlesFts.FromSqlRaw("INSERT INTO Titles_fts (AnimeId, Title, Type) SELECT AnimeId, Title, Type FROM TitleEntries;"));
    }

    /// <summary>
    /// Searches for titles in the database using different methods
    /// </summary>
    /// <param name="searchText">Text to search</param>
    /// <returns>List of matching titles</returns>
    [SuppressMessage("Security", "EF1002:Risk of vulnerability to SQL injection.")]
    private static List<TitleFtsDto> SearchTitles(string searchText)
    {
        List<TitleFtsDto> matchingTitles = new List<TitleFtsDto>();
        string safeSearchText = searchText.Replace("'", "\"\"");
        if (safeSearchText.Contains(',') || safeSearchText.Contains('-') || safeSearchText.Contains(':') || safeSearchText.Contains('!') || safeSearchText.Contains('?') || safeSearchText.Contains('.') || safeSearchText.Contains('#') || safeSearchText.Contains('@')) safeSearchText = "\"" + safeSearchText + "\"";
        
        // Conventional Search
        var conventionalSearch = Context.TitlesFts.FromSqlRaw($"SELECT * FROM Titles_fts WHERE Title MATCH '{safeSearchText}';").ToList();
        if (conventionalSearch.Count > 0) return conventionalSearch;
        
        // Filtered Split Search
        var splitTextver1 = safeSearchText.Split(" ");
        var exclusionWords = JsonFileUtility.GetVerbsList(FileHandler.GetFileInProgramFolder("ExclusionWords.json"));
        
        foreach (var split in splitTextver1)
        {
            if (exclusionWords.Any(x => x == split.Trim().ToLower())) continue;
            var anime = Context.TitlesFts.FromSql($"SELECT * FROM Titles_fts WHERE Title MATCH '{split}*';").ToList();
            matchingTitles.AddRange(anime); 
        }
        if (matchingTitles.Count > 0) return matchingTitles;
        
        
        // Split Search
        var splitTextver2 = safeSearchText.SplitIntoChunks(int.Parse(SettingsManager.GetSetting("Execution Settings", "StringSplitRange")));
        foreach (var split in splitTextver2)
        {
            ConsoleExt.WriteLineWithPretext(split);
            var
                anime = Context.TitlesFts.FromSql($"SELECT * FROM Titles_fts WHERE Title MATCH '{split}*';")
                    .ToList(); // if it's the last string in the split, it will not add * at the end or add * before the last string
            matchingTitles.AddRange(anime);
        }
        return matchingTitles;
    }
    
    /// <summary>
    /// Gets Anime by title
    /// </summary>
    /// <param name="animeTitle">Anime title</param>
    /// <returns>Anime</returns>
    public static List<AnimeDto> GetAnimeByTitle(string animeTitle)
    {
        var titles = SearchTitles(animeTitle);
        List<long> malIds = new List<long>();
        foreach (var title in titles)
        {
            malIds.Add(title.AnimeId);
        }
        return GetAnimesByIds(malIds).Values.ToList();
    }

    /// <summary>
    /// Gets the last Myanimelist id from the database
    /// </summary>
    /// <returns>Myanimelist id</returns>
    private static long? GetLastMalId()
    {
        return Context.Animes.Max(a => a.MalId);
    }

    /// <summary>
    /// Gets the latest animes from Myanimelist
    /// </summary>
    public static async void GetLatestAnimes()
    {
        try
        {
            var lastMalId = (int)GetLastMalId()!;
            ConsoleExt.WriteLineWithPretext($"Getting the latest anime with malId: {lastMalId}");
            await JikanHandler.StartSearch(lastMalId, 1000);
        }
        catch (Exception e)
        {
            ConsoleExt.WriteLineWithPretext("An Error has occured while getting the the latest anime: ", ConsoleExt.OutputType.Error, e);
        }
    }
}