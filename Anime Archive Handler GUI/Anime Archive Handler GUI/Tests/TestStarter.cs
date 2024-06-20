namespace Anime_Archive_Handler_GUI.Tests;

public class TestStarter
{
    public static void StartTest()
    {
        string sqlitePath = FileHandler.GetFileInProgramFolder("SQLite.db");
        string litedbPath = FileHandler.GetFileInProgramFolder("Litedb.db");

        var performanceTest = new PerformanceTest(sqlitePath, litedbPath);
        performanceTest.RunTest();
    }

    public static void PortDatabase()
    {
        string sourceLiteDbPath = FileHandler.GetFileInProgramFolder("DataBase.db");
        string destinationLiteDbPath = FileHandler.GetFileInProgramFolder("Litedb.db");
        string sqliteDbPath = FileHandler.GetFileInProgramFolder("SQLite.db");

        // Port LiteDB to SQLite
        var dbPorter = new DatabasePorter(sourceLiteDbPath, sqliteDbPath);
        dbPorter.PortLiteDbToSqlite();

        // Copy LiteDB to another LiteDB
        var liteDbPorter = new LiteDbPorter(sourceLiteDbPath, destinationLiteDbPath);
        liteDbPorter.CopyLiteDbToLiteDb();
    }
}