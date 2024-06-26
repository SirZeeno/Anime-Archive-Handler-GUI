using LiteDB;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Anime_Archive_Handler_GUI.Tests;

public class DatabasePorter
{
    private readonly AnimeContext _context;

    public DatabasePorter()
    {
        _context = new AnimeContext();
    }
    
    public void DeleteAll()
    {
        _context.Database.ExecuteSqlRaw("DELETE FROM Animes");
        _context.Database.ExecuteSqlRaw("DELETE FROM MalUrls");
        _context.Database.ExecuteSqlRaw("DELETE FROM AnimeTrailers");
        _context.Database.ExecuteSqlRaw("DELETE FROM ImagesSets");
        _context.Database.ExecuteSqlRaw("DELETE FROM AnimeBroadcasts");
        _context.Database.ExecuteSqlRaw("DELETE FROM TitleEntries");
        _context.Database.ExecuteSqlRaw("DELETE FROM TitleSynonyms");
        _context.Database.ExecuteSqlRaw("DELETE FROM ImageDto");
        _context.Database.ExecuteSqlRaw("DELETE FROM TimePeriodDto");
        _context.SaveChanges();
    }

    public void PortData(List<AnimeDto> animes) //TODO: check if they have already been added
    {
        // Insert data into SQLite using EF Core
        _context.Animes.AddRange(animes);

        // Ensure other related data is added similarly, e.g., Producers, Licensors, etc.
        foreach (var anime in animes)
        {
            _context.Producers.AddRange(anime.Producers);
            _context.Licensors.AddRange(anime.Licensors);
            _context.Studios.AddRange(anime.Studios);
            _context.Genres.AddRange(anime.Genres);
            _context.ExplicitGenres.AddRange(anime.ExplicitGenres);
            _context.Themes.AddRange(anime.Themes);
            _context.Demographics.AddRange(anime.Demographics);
            _context.TitleSynonyms.AddRange(anime.TitleSynonyms);
            _context.TitleEntries.AddRange(anime.Titles);
        }

        _context.SaveChanges();
    }
}

public class LiteDBReader
{
    private static readonly string _liteDbPath = "F:\\Rider Projects\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Databases\\Database.db";

    public static List<AnimeDtodb> ReadAnimes()
    {
        using var db = new LiteDatabase(_liteDbPath);
        var animeCollection = db.GetCollection<AnimeDtodb>("Anime");
        return animeCollection.FindAll().ToList();
    }
}