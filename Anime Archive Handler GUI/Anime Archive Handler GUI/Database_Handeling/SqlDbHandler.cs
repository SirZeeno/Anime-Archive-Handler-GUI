using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Anime_Archive_Handler_GUI.Database_Handeling;

public static class SqlDbHandler
{
    public static readonly AnimeContext Context = new AnimeContext();
    
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

    private static async Task<Dictionary<long?, ImagesSetDto>> GetAnimeImagesByIds(List<long?> malIds)
    {
        await using var sqliteContext = new AnimeContext();

        var animes = new List<AnimeDto>();
        await Task.Factory.StartNew(() =>
        {
            animes = sqliteContext.Animes
                .Include(a => a.Images)
                .ThenInclude(i => i.JPG)
                .Include(a => a.Images)
                .ThenInclude(i => i.WebP)
                .Where(a => malIds.Contains(a.MalId))
                .ToList();
        });
        return animes.ToDictionary(a => a.MalId, a => a.Images);
    }
    
    public static async Task<Dictionary<long?, AnimeImageSetBitmap>> GetAnimeBitmapImagesByIds(List<long?> malIds)
    {
        await using var sqliteContext = new AnimeContext();
        var animes = new List<AnimeImageSetBitmap>();

        await Task.Factory.StartNew(() =>
        {
            animes = Enumerable.ToList(sqliteContext.ImageBitmaps
                    .Include(a => a.JPG)
                    .Include(a => a.WebP)
                    .Where(a => malIds.Contains(a.MalId)));
        });
        return animes.ToDictionary(a => a.MalId, a => a);
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

    private static Dictionary<long?, AnimeDto> GetAnimesByIds(List<long?> malIds)
    {
        return Context.Animes.Where(a => malIds.Contains(a.MalId))
            .Include(a => a.ImageBitmaps)
            .ThenInclude(i => i.JPG)
            .Include(a => a.ImageBitmaps)
            .ThenInclude(i => i.WebP)
            .Include(a => a.Titles)
            .Include(x => x.Genres)
            .Include(x => x.ExplicitGenres)
            .Include(x => x.Producers)
            .Include(x => x.Licensors)
            .Include(x => x.Studios)
            .ToDictionary(a => a.MalId, a => a);
    }
    
    // Gets a specific anime by id
    public static AnimeDto? GetAnimeById(long malId)
    {
        return Context.Animes.Find(malId);
    }

    // Processes all images in the database into bitmaps
    public static async void ProcessAllImages(List<long?>? Ids = null)
    {
        int animeCount = (int)Context.Animes.Max(a => a.MalId)!;
        List<long?> malIds = Ids ?? Enumerable.Range(1, animeCount).ToList().ConvertAll(i => (long?) i);
        var images = await Task.Run(() => GetAnimeImagesByIds(malIds).GetAwaiter().GetResult());
        ConsoleExt.WriteLineWithPretext("Got all images", ConsoleExt.OutputType.Info);
        List<AnimeImageSetBitmap> animeImageSetBitmaps = new List<AnimeImageSetBitmap>();
        foreach (var image in images)
        {
            ConsoleExt.WriteLineWithPretext("Processing images for malId: " + image.Key, ConsoleExt.OutputType.Info);
            
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
        CorrectAnimeImageRelations(Ids);
        ConsoleExt.WriteLineWithPretext("Processed all images", ConsoleExt.OutputType.Info);
    }

    public static async void CorrectAnimeImageRelations(List<long?>? Ids = null)
    {
        int animeCount = (int)Context.Animes.Max(a => a.MalId)!;
        List<long?> malIds = Ids ?? Enumerable.Range(1, animeCount).ToList().ConvertAll(i => (long?) i);
        var animes = GetAnimesByIds(malIds);
        foreach (var anime in animes.Values)
        {
            long? malId = anime.MalId;
            var images = await GetAnimeBitmapImagesByIds([malId]);
            anime.ImageBitmaps = images[malId];
            anime.AnimeImageSetBitmapId = images[malId].MalId;
        }
        ConsoleExt.WriteLineWithPretext("Done correcting anime image relations", ConsoleExt.OutputType.Info);
        await Context.SaveChangesAsync();
    }
    
    public static IQueryable<AnimeDto> GetAnimesByCount(int count)
    {
        return Context.Animes.Take(count);
    }
    
    // Updates the fts table from the title entries
    public static void UpdateTitleFts()
    {
        Context.Database.EnsureCreated();
        Context.TitlesFts.FromSqlRaw("drop table Titles_fts;");
        Context.TitlesFts.FromSqlRaw("CREATE VIRTUAL TABLE Titles_fts USING fts5(AnimeId UNINDEXED, Title, Type UNINDEXED);");
        Task.Run(() => Context.TitlesFts.FromSqlRaw("INSERT INTO Titles_fts (AnimeId, Title, Type) SELECT AnimeId, Title, Type FROM TitleEntries;"));
    }

    private static List<TitleFtsDto> SearchTitles(string searchText) // runs through different methods to search for the title
    {
        List<TitleFtsDto> matchingTitles = new List<TitleFtsDto>();
        string safeSearchText = searchText.Replace("'", "\"\"");
        if (safeSearchText.Contains(',') || safeSearchText.Contains('-') || safeSearchText.Contains(':') || safeSearchText.Contains('.')) safeSearchText = "\"" + safeSearchText + "\"";
        
        // Conventional Search
        var conventionalSearch = Context.TitlesFts.FromSqlRaw($"SELECT * FROM Titles_fts WHERE Title MATCH '{safeSearchText}';").ToList();
        if (conventionalSearch.Count > 0) return conventionalSearch;
        
        // Filtered Split Search
        var splitTextver1 = safeSearchText.Split(" ");
        var exclusionWords = JsonFileUtility.GetVerbsList(FileHandler.GetFileInProgramFolder("ExclusionWords.json"));
        
        foreach (var split in splitTextver1)
        {
            if (exclusionWords.Where(x => x == split.Trim().ToLower()).Any()) continue;
            var anime = Context.TitlesFts.FromSqlRaw($"SELECT * FROM Titles_fts WHERE Title MATCH '{split}*';").ToList();
            matchingTitles.AddRange(anime);
        }
        if (matchingTitles.Count > 0) return matchingTitles;
        
        
        // Split Search
        var splitTextver2 = safeSearchText.SplitIntoChunks(int.Parse(SettingsManager.GetSetting("Execution Settings", "StringSplitRange")));
        foreach (var split in splitTextver2)
        {
            ConsoleExt.WriteLineWithPretext(split, ConsoleExt.OutputType.Info);
            var anime = Context.TitlesFts.FromSqlRaw($"SELECT * FROM Titles_fts WHERE Title MATCH '{split}*';").ToList(); // if it's the last string in the split, it will not add * at the end or add * before the last string
            matchingTitles.AddRange(anime);
        }
        return matchingTitles;
    }
    
    public static List<AnimeDto> GetAnimeByTitle(string animeTitle)
    {
        var titles = SearchTitles(animeTitle);
        List<long?> malIds = new List<long?>();
        foreach (var title in titles)
        {
            malIds.Add(title.AnimeId);
        }
        return GetAnimesByIds(malIds).Values.ToList();
    }

    private static long? GetLastMalId()
    {
        return Context.Animes.Max(a => a.MalId);
    }

    public static async void GetLatestAnimes()
    {
        long? lastMalId = GetLastMalId();
        await JikanHandler.StartSearch((int)lastMalId, 1000);
    }
}