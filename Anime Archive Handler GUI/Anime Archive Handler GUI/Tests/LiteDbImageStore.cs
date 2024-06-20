using LiteDB;
using System.IO;

namespace Anime_Archive_Handler_GUI.Tests;

public class LiteDbImageStore
{
    private readonly ILiteCollection<AnimeDto> _collection;

    public LiteDbImageStore(string dbPath)
    {
        var db = new LiteDatabase($"Filename={dbPath};Connection=shared");
        _collection = db.GetCollection<AnimeDto>("images");
    }

    public void AddImage(AnimeDto image)
    {
        _collection.Insert(image);
    }

    public AnimeDto GetImage(int id)
    {
        return _collection.FindOne(anime => anime.MalId == id);
    }

    public void UpdateImage(AnimeDto image)
    {
        _collection.Update(image);
    }

    public void DeleteImage(long? id)
    {
        _collection.Delete(id);
    }
    
    public AnimeDto GetAnimebyId(int id)
    {
        return _collection.FindOne(x => x.MalId == id);
    }
}
