// do namespace names or values matter???

using System.IO;

namespace Anime_Archive_Handler_GUI.Interfaces;
using static AnimeArchiveHandler;
using static CommonSettings;

public interface IDirectoryCreation
{
    string Location { get;}
    static abstract void CreateDirectory();
}

// need to figure out how to access a non-static variable in a static function and class
public class CreateHentaiDirectory : IDirectoryCreation
{
    public string Location => SettingsManager.GetSetting("Output Paths", "HentaiOutputFolder"); //the user set location or default location

    public static void CreateDirectory()
    {
        
    }
}

public class CreateMangaDirectory : IDirectoryCreation
{
    public string Location => SettingsManager.GetSetting("Output Paths", "MangaOutputFolder"); //the user set location or default location

    public static void CreateDirectory()
    {
        
    }
}

public class CreateAnimeDirectory : IDirectoryCreation
{
    public string Location => SettingsManager.GetSetting("Output Paths", "AnimeOutputFolder"); //the user set location or default location
    private static readonly Language? SubOrDub = GetSubOrDub();
    private static readonly string? AnimeName = GetAnimeName();
    private static readonly int[]? SeasonNumbers = GetSeasonNumbers();
    private static readonly int[]? PartNumbers = GetPartNumbers();
    
    /*
        if (_subOrDub == null || _animeName == null || _seasonNumbers!.Length == 0)
        {
            ConsoleExt.WriteLineWithPretext($"Anime Name: {_animeName}, Sub or Dub: {_subOrDub.ToString()}, or Season Number: {_seasonNumbers!.Length} is null",
                ConsoleExt.OutputType.Error);
            return;
        }

        if (!Directory.Exists(AnimeOutputFolder)) Directory.CreateDirectory(AnimeOutputFolder);
        if (!Directory.Exists(Path.Combine(AnimeOutputFolder, _subOrDub.ToString()!)))
            Directory.CreateDirectory(Path.Combine(AnimeOutputFolder, _subOrDub.ToString()!));
        if (!Directory.Exists(Path.Combine(AnimeOutputFolder, _subOrDub.ToString()!, _animeName)))
            Directory.CreateDirectory(Path.Combine(AnimeOutputFolder, _subOrDub.ToString()!, _animeName));

        foreach (var season in _seasonNumbers)
        {
            if (Directory.Exists(Path.Combine(AnimeOutputFolder, _subOrDub.ToString()!, _animeName, @"\Season ",
                    season.ToString())))
                return;
            Directory.CreateDirectory(Path.Combine(AnimeOutputFolder, _subOrDub.ToString()!, _animeName, @"\Season ",
                season.ToString()));
        }
     */

    public static void CreateDirectory()
    {
        if (SubOrDub == null || AnimeName == null || SeasonNumbers!.Length == 0 || PartNumbers!.Length == 0)
        {
            ConsoleExt.WriteLineWithPretext($"Anime Name: {AnimeName}, Sub or Dub: {SubOrDub.ToString()}, or Season Numbers: {SeasonNumbers!.Length}, or Part Numbers: {PartNumbers!.Length} is null", ConsoleExt.OutputType.Error);
            return;
        }
        
        if (!Directory.Exists(AnimeOutputFolder)) Directory.CreateDirectory(AnimeOutputFolder);
        if (!Directory.Exists(Path.Combine(AnimeOutputFolder, SubOrDub.ToString()!)))
            Directory.CreateDirectory(Path.Combine(AnimeOutputFolder, SubOrDub.ToString()!));
    }
}