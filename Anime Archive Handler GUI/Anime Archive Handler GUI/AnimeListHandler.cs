using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Anime_Archive_Handler_GUI.Interfaces;
using Spectre.Console;

namespace Anime_Archive_Handler_GUI;

using static InputStringHandler;
using static DbHandler;
using static FileHandler;
using static SettingsManager;
using static CommonSettings;

public static class AnimeListHandler
{
    private static string _animeList = GetSetting("Output Paths", "AnimeListOutput");

    private static readonly string AnimeListBackup = GetFileInProgramFolder("AnimeList.db");
    private static List<AnimeDto?>? _anime;
    private static int[]? _seasonNumber;

    public static void StartAnimeListEditing()
    {
        if (string.IsNullOrEmpty(_animeList)) _animeList = AnimeListBackup;

        ConsoleExt.WriteLineWithPretext($"Anime List is Stored at: {_animeList}", ConsoleExt.OutputType.Info);
        CheckFileExistence(_animeList);

        // Create the layout
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Console"),
                new Layout("Results")
                    .SplitRows(
                        new Layout("Selection"),
                        new Layout("Logs")));
        
        AnsiConsole.Write(layout);
        
        while (true)
        {
            _anime = new List<AnimeDto?>();
            ConsoleExt.WriteLineWithPretext("What Anime would you like to Add/Remove to the List? (+/- before the link or name)",
                ConsoleExt.OutputType.Question);
            Console.Write("Anime Name or URL: ");

            var inputString = Console.ReadLine();
            string pattern = Regex.Escape("Anime Name or URL: ");
            if (inputString == null) return;
            var cutInputString = Regex.Replace(inputString, pattern, "");
            var animeName = CheckIfUrl(cutInputString);

            _seasonNumber = ExtractingSeasonNumber(animeName);

            // Adds the anime to the list
            if (cutInputString.StartsWith("+"))
            {
                AddAnime(animeName);
            }

            // Removes the anime from the list
            if (cutInputString.StartsWith("-"))
            {
                RemoveAnime(animeName);
            }

            //if nothing is there then its going to look them up in the database and let the user decide what to do depending on the result
            else
            {
                var animeExistences = CheckAnimeExistence(GetAnimesWithTitle(animeName)?.First().MalId);
                if (animeExistences)
                {
                    if (!HelperClass.ManualInformationChecking(
                            "Anime already exists in the Anime List! Would you like to remove it?")) continue;
                    RemoveAnime(animeName);
                }
                else
                {
                    if (!HelperClass.ManualInformationChecking(
                            "Anime does NOT exist in the Anime List! Would you like to add it?")) continue;
                    AddAnime(animeName);
                }
            }
        }
    }

    private static string CheckIfUrl(string cutInputString)
    {
        if (!cutInputString.Contains("https://") && !cutInputString.Contains("http://")) return cutInputString;
        var animeName = UrlNameExtractor(cutInputString);
        ConsoleExt.WriteLineWithPretext($"Anime Name: {animeName}", ConsoleExt.OutputType.Info);
        return animeName;

    }

    private static async void AddAnime(string? animeName)
    {
        if (HeadlessOperations)
        {
            if (animeName != null) _anime?.Add(GetAnimesWithTitle(RemoveUnnecessaryNamePieces(animeName))!.First());
        }
        else
        {
            if (animeName != null) _anime?.AddRange(GetAnimesWithTitle(RemoveUnnecessaryNamePieces(animeName))!);
        }
        
        if (_anime == null) return;
        foreach (var anime in _anime.Where(anime => anime != null))
        {
            if (anime != null) await SaveToDb(anime, new EditAnimeList());
        }
    }

    private static void RemoveAnime(string? animeName)
    {
        if (HeadlessOperations)
        {
            if (animeName != null) _anime?.Add(GetAnimesWithTitle(RemoveUnnecessaryNamePieces(animeName))!.First());
        }
        else
        {
            if (animeName != null) _anime?.AddRange(GetAnimesWithTitle(RemoveUnnecessaryNamePieces(animeName))!);
        }

        if (_anime == null) return;
        foreach (var anime in _anime.Where(anime => anime != null))
        {
            if (anime != null) RemoveFromDb((long)anime.MalId!, new EditAnimeList());
        }
    }

}