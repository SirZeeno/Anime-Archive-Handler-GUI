using System;
using System.Linq;
using LiteDB;

namespace Anime_Archive_Handler_GUI.Tests;

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
        // Open source LiteDB
        using var sourceLiteDb = new LiteDatabase($"Filename={_sourceLiteDbPath};Connection=shared");
        var sourceCollection = sourceLiteDb.GetCollection<AnimeDto>("images");

        // Open destination LiteDB
        using var destinationLiteDb = new LiteDatabase($"Filename={_destinationLiteDbPath};Connection=shared");
        var destinationCollection = destinationLiteDb.GetCollection<AnimeDto>("images");

        // Get all images from source LiteDB
        var images = sourceCollection.FindAll().ToList();

        // Add images to destination LiteDB
        destinationCollection.InsertBulk(images);
        
        Console.WriteLine($"Successfully copied {images.Count} images from source LiteDB to destination LiteDB.");
    }
}
