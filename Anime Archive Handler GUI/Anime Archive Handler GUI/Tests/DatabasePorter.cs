using LiteDB;
using System;
using System.Linq;

namespace Anime_Archive_Handler_GUI.Tests;

public class DatabasePorter
{
    private readonly string _liteDbPath;
    private readonly string _sqliteDbPath;

    public DatabasePorter(string liteDbPath, string sqliteDbPath)
    {
        _liteDbPath = liteDbPath;
        _sqliteDbPath = sqliteDbPath;
    }

    public void PortLiteDbToSqlite()
    {
        // Create SQLite context
        using var sqliteContext = new AnimeContext(_sqliteDbPath);
        sqliteContext.Database.EnsureCreated();

        // Open LiteDB database
        using var liteDb = new LiteDatabase($"Filename={_liteDbPath};Connection=shared");
        var liteCollection = liteDb.GetCollection<AnimeDto>("images");

        // Get all images from LiteDB
        var animeDtos = liteCollection.FindAll().ToList();

        // Add images to SQLite
        sqliteContext.Animes.AddRange(animeDtos);
        sqliteContext.SaveChanges();
        
        Console.WriteLine($"Successfully ported {animeDtos.Count} images from LiteDB to SQLite.");
    }
}
