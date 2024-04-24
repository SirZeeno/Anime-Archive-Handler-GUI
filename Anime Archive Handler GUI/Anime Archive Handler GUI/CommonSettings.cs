using LiteDB;

namespace Anime_Archive_Handler_GUI;
using static FileHandler;
using static SettingsManager;

public class CommonSettings
{
    internal static readonly string SettingsPath = GoGetter();
    internal static readonly string UserSettingsFile = GetFileInProgramFolder("UserSettings.ini"); // needs to initialize after the settings path
    public static readonly string AnimeOutputFolder = GetSetting("Output Paths", "AnimeOutputFolder");
    public static readonly bool HeadlessOperations = bool.Parse(GetSetting("Execution Settings", "HeadlessOperations"));
    
    private static readonly LiteDatabase Db = new(GetFileInProgramFolder("DataBase.db")); // need to cache those in the program settings and only search for them again if it cant be found in the same location
    private static readonly LiteDatabase Al = new(GetFileInProgramFolder("AnimeList.db"));
    private static readonly LiteDatabase Ats = new(GetFileInProgramFolder("Animetosho.db"));
    private static readonly LiteDatabase Nh = new(GetFileInProgramFolder("Nhentai.db"));
    //private static readonly LiteDatabase Ts = new(GetFileInProgramFolder("TestDatabase.db")); // for testing purposes only
    
    public static readonly ILiteCollection<AnimeDto> AnimeDb = Db.GetCollection<AnimeDto>("Anime"); //loads anime database
    public static readonly ILiteCollection<TitleEntryDb> TitleEntryListDb = Db.GetCollection<TitleEntryDb>("TitleEntry");
    public static readonly ILiteCollection<AnimeDto> ToWatchListDb = Al.GetCollection<AnimeDto>("ToWatch");
    public static readonly ILiteCollection<TitleEntryDb> ToWatchListTitlesDb = Al.GetCollection<TitleEntryDb>("ToWatchTitleEntry");
    public static readonly ILiteCollection<Animetosho> AnimetoshoDb = Ats.GetCollection<Animetosho>("Animetosho");
    internal static readonly ILiteCollection<NHentaiMetaData> NhentaiDb = Nh.GetCollection<NHentaiMetaData>("NHentaiMetaData");
    //private static readonly ILiteCollection<AnimeDto> AnimeDtoTestDb = Ts.GetCollection<AnimeDto>("AnimeDto"); // for testing purposes only
    
    private static Language? _subOrDub;
    public static Language? GetSubOrDub() { return _subOrDub;}
    public static void SetSubOrDub(Language subOrDub) { _subOrDub = subOrDub;}
    
}

public enum Language
{
    Sub,
    Dub
}