using System;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace Anime_Archive_Handler_GUI.Tests;

public class PerformanceTest
{
    private readonly AnimeContext _sqliteContext;
    private readonly LiteDbImageStore _liteDbStore;

    public PerformanceTest(string sqlitePath, string litedbPath)
    {
        _sqliteContext = new AnimeContext(sqlitePath);
        _sqliteContext.Database.EnsureCreated();

        _liteDbStore = new LiteDbImageStore(litedbPath);
    }

    private AnimeDto GetAnimeById(int malId)
    {
        return _sqliteContext.Animes.FirstOrDefault(anime => anime.MalId == malId) ?? throw new InvalidOperationException();
    }

    public void RunTest()
    {
        // SQLite test
        var sqliteWatch = Stopwatch.StartNew();
        var sqliteImage = _sqliteContext.Animes.Find(17829);
        sqliteWatch.Stop();
        Console.WriteLine($"SQLite Read Time: {sqliteWatch.ElapsedMilliseconds} ms");

        sqliteWatch.Restart();
        _sqliteContext.Animes.Remove(GetAnimeById(17829));
        _sqliteContext.SaveChanges();
        sqliteWatch.Stop();
        Console.WriteLine($"SQLite Delete Time: {sqliteWatch.ElapsedMilliseconds} ms");

        // LiteDB test
        var litedbWatch = Stopwatch.StartNew();
        var liteDbImage = _liteDbStore.GetImage(17829);
        litedbWatch.Stop();
        Console.WriteLine($"LiteDB Read Time: {litedbWatch.ElapsedMilliseconds} ms");
        
        litedbWatch.Restart();
        _liteDbStore.DeleteImage(_liteDbStore.GetAnimebyId(17829).MalId);
        litedbWatch.Stop();
        Console.WriteLine($"LiteDB Delete Time: {litedbWatch.ElapsedMilliseconds} ms");
    }
}
