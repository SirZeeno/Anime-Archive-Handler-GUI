using System;
using System.Linq;
using LiteDB;

namespace Anime_Archive_Handler_GUI.Database_Handeling;

public class LiteDbPorter
{
    private readonly string _sourceLiteDbPath;
    private readonly string _destinationLiteDbPath;

    public LiteDbPorter(string sourceLiteDbPath, string destinationLiteDbPath)
    {
        _sourceLiteDbPath = sourceLiteDbPath;
        _destinationLiteDbPath = destinationLiteDbPath;
    }

    public void CopyLiteDbToLiteDb()
    {
        // Open destination LiteDB
        using var destinationLiteDb = new LiteDatabase($"Filename={_destinationLiteDbPath};Connection=shared");
        var destinationCollection = destinationLiteDb.GetCollection<AnimeDto>("Anime");
        
        var animes = LiteDBReader.ReadAnimes().Select(x => new AnimeService(new AnimeMapper()).GetAnimeDto(x)).ToList();

        // Add images to destination LiteDB
        destinationCollection.InsertBulk(animes);
        
        Console.WriteLine($"Successfully copied {animes.Count} images from source LiteDB to destination LiteDB.");
    }
}
