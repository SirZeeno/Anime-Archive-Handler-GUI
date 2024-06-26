using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

    private AnimeDto? GetAnimeById(long malId)
    {
        using var _sqliteContext2 = new AnimeContext();
        _sqliteContext2.Database.EnsureCreated();
        return _sqliteContext2.Animes.Find(malId);
    }

    public void RunTest()
    {
        // SQLite test
        var sqliteWatch = Stopwatch.StartNew();
        var sqliteImage = GetAnimeById(29503);
        sqliteWatch.Stop();
        Console.WriteLine(sqliteImage.MalId);
        Console.WriteLine($"SQLite Read Time: {sqliteWatch.ElapsedMilliseconds} ms");

        // LiteDB test
        var litedbWatch = Stopwatch.StartNew();
        var liteDbImage = _liteDbStore.GetImage(29503);
        litedbWatch.Stop();
        Console.WriteLine(liteDbImage.MalId);
        Console.WriteLine($"LiteDB Read Time: {litedbWatch.ElapsedMilliseconds} ms");
    }
}
