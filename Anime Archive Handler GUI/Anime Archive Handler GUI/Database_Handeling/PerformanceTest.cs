using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Anime_Archive_Handler_GUI.Database_Handeling;
using Microsoft.EntityFrameworkCore;


namespace Anime_Archive_Handler_GUI.Tests;

public class PerformanceTest
{
    private readonly AnimeContext _sqliteContext;
    private readonly LiteDbImageStore _liteDbStore;

    public PerformanceTest(string sqlitePath, string litedbPath)
    {
        _sqliteContext = new AnimeContext();
        _sqliteContext.Database.EnsureCreated();

        _liteDbStore = new LiteDbImageStore(litedbPath);
    }

    private ICollection<TitleEntryDto> GetAnimeById(long malId)
    {
        using var sqliteContext = new AnimeContext();
        sqliteContext.Database.EnsureCreated();
        var anime = sqliteContext.Animes.Find(malId);
        sqliteContext.Entry(anime).Collection("Titles").Load();
        return anime.Titles;
    }

    public void RunTest()
    {
        // SQLite test
        var sqliteWatch = Stopwatch.StartNew();
        var sqliteImage = GetAnimeById(29503);
        sqliteWatch.Stop();
        Console.WriteLine(sqliteImage.Count);
        foreach (var title in sqliteImage)
        {
            Console.WriteLine(title.Type);
            Console.WriteLine(title.Title);
        }
        Console.WriteLine($"SQLite Read Time: {sqliteWatch.ElapsedMilliseconds} ms");
        
        sqliteWatch.Restart();
        var animeTitle = SqlDbHandler.SearchTitles("Gararin to Garorin");
        sqliteWatch.Stop();
        foreach (var animetitle in animeTitle)
        {
            Console.WriteLine(animetitle.Title);
        }
        Console.WriteLine($"SQLite Title Read Time: {sqliteWatch.ElapsedMilliseconds} ms");
        
        // LiteDB test
        var litedbWatch = Stopwatch.StartNew();
        var liteDbImage = _liteDbStore.GetImage(29503);
        litedbWatch.Stop();
        Console.WriteLine(sqliteImage.Count);
        Console.WriteLine($"LiteDB Read Time: {litedbWatch.ElapsedMilliseconds} ms");
    }
}
