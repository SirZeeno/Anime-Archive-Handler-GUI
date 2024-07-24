using System.Linq;

namespace Anime_Archive_Handler_GUI.Database_Handeling;

public static class TestStarter
{
    public static void PortDatabase()
    {
        string sourceLiteDbPath = FileHandler.GetFileInProgramFolder("DataBase.db");
        string destinationLiteDbPath = "F:\\Rider Projects\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Databases\\Litedb.db";
        string sqliteDbPath = "F:\\Rider Projects\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Databases\\SQLiteTest.db";

        // Port LiteDB to SQLite
        var dbPorter = new DatabasePorter();
        dbPorter.DeleteAll();
        dbPorter.PortData(LiteDBReader.ReadAnimes().Select(x => new AnimeService(new AnimeMapper()).GetAnimeDto(x)).ToList());

        // Copy LiteDB to another LiteDB
        //var liteDbPorter = new LiteDbPorter(sourceLiteDbPath, destinationLiteDbPath);
        //liteDbPorter.CopyLiteDbToLiteDb();
    }
}