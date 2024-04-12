using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Humanizer;
using static System.Text.RegularExpressions.Regex;

namespace Anime_Archive_Handler_GUI.Desktop;
using static AnimeArchiveHandler;
using static HelperClass;
using static FileHandler;
using static CommonSettings;

public static partial class InputStringHandler
{
    //removes all unnecessary pieces from the anime name
    internal static string RemoveUnnecessaryNamePieces(string fileName)
    {
        var withoutBrackets = MyRegex6().Replace(fileName, string.Empty);
        var withoutUnderscore = MyRegex7().Replace(withoutBrackets, " ");
        var withoutSeason = MyRegex8().Replace(withoutUnderscore, " ");
        var withoutOrdinal = MyRegex9().Replace(withoutSeason, string.Empty);
        var withoutRomanNumerals = MyRegex10().Replace(withoutOrdinal, string.Empty);
        var withoutAfterSpaces = MyRegex11().Replace(withoutRomanNumerals, string.Empty); //removes everything after 2 spaces

        var output = withoutAfterSpaces.ApplyCase(LetterCasing.Title).TrimStart().TrimEnd();
        ConsoleExt.WriteLineWithPretext(output, ConsoleExt.OutputType.Info);
        return output;
    }
    
    public static string UrlNameExtractor(string? inputUrl)
    {
        if (inputUrl == null)
        {
            ConsoleExt.WriteLineWithPretext("Tried to extract Url but input was null!", ConsoleExt.OutputType.Error);
            return string.Empty;
        }

        if (inputUrl.Contains("gogoanime"))
        {
            var removedWebsite = MyRegex12().Replace(inputUrl, "");
            var removedEpisode = MyRegex13().Replace(removedWebsite, "");
            var removedDashes = MyRegex14().Replace(removedEpisode, " ");
            var removedLastWord = MyRegex15().Replace(removedDashes, "");
            ConsoleExt.WriteLineWithPretext(removedLastWord.Trim(), ConsoleExt.OutputType.Info);

            return removedLastWord.Trim();
        }

        if (inputUrl.Contains("anix"))
        {
            var removedWebsite = MyRegex16().Replace(inputUrl, "");
            var removedEpisode = MyRegex13().Replace(removedWebsite, "");
            var removedDashes = MyRegex14().Replace(removedEpisode, " ");
            var removedLastWord = MyRegex15().Replace(removedDashes, "");
            ConsoleExt.WriteLineWithPretext(removedLastWord.Trim(), ConsoleExt.OutputType.Info);

            return removedLastWord.Trim();
        }
        return string.Empty;
    }

    //Checks if the Folder Name indicates if the anime has and OVA
    internal static bool HasOva(string folderName)
    {
        
        return false;
    }

    //Checks if the Folder Name indicates if the anime has multiple parts
    internal static bool HasMultipleParts(string fileName)
    {
        var match1 = MyRegex20().Match(fileName);
        var match2 = MyRegex21().Match(fileName);

        if (match1.Success || match2.Success) return true;
        ConsoleExt.WriteLineWithPretext("No Anime Season Part Found!", ConsoleExt.OutputType.Info);
        return false;
    }
    
    //extracts the season number from the folder name
    internal static int[] ExtractingSeasonNumber(string fileName)
    {
        var match1 = MyRegex().Match(fileName);
        var match2 = MyRegex1().Match(fileName);
        var match4 = MyRegex2().Match(fileName);
        var match5 = MyRegex3().Match(fileName);

        int[] seasonNumber = [];

        if (!match1.Success && !match2.Success && !match4.Success && !match5.Success)
        {
            seasonNumber = [1];
            ConsoleExt.WriteLineWithPretext("No Anime Season number Found!", ConsoleExt.OutputType.Warning);
            if (!HeadlessOperations)
            {
                ManualSeasonNumber("What Anime Season is it?");
            }
            ConsoleExt.WriteLineWithPretext($"Season Number: {seasonNumber[0]}", ConsoleExt.OutputType.Info);
            return seasonNumber;
        }

        var match3 = MyRegex4().Match(match1.Value);

        if (!match3.Success)
        {
            seasonNumber = [1];
            if (int.TryParse(new string(match1.Value.Where(char.IsDigit).ToArray()), out seasonNumber[0]))
            {
            }
            else
            {
                if (int.TryParse(new string(match4.Value.Where(char.IsDigit).ToArray()), out seasonNumber[0]))
                {
                }
                else
                {
                    if (int.TryParse(new string(match2.Value.Where(char.IsDigit).ToArray()), out seasonNumber[0]))
                    {
                    }
                    else
                    {
                        if (int.TryParse(ConvertRomanToNumber(match5.Value).ToString(), out seasonNumber[0]))
                        {
                        }
                    }
                }
            }

            ConsoleExt.WriteLineWithPretext($"Season Number: {seasonNumber[0]}", ConsoleExt.OutputType.Info);
            return seasonNumber;
        }

        var seasonNumbers = new List<int>();
        foreach (Match match in MyRegex5().Matches(match1.ToString()))
            seasonNumbers.Add(Convert.ToInt32(match.Value));
        switch (match3.ToString())
        {
            case "+":
            {
                seasonNumber = seasonNumbers.ToArray();
                ConsoleExt.WriteWithPretext($"Season Numbers: {seasonNumber[0]}", ConsoleExt.OutputType.Info);
                foreach (var number in seasonNumber)
                    if (number != seasonNumber[0])
                        Console.Write(", " + number);
                Console.WriteLine();
                return seasonNumbers.ToArray();
            }
            case "-":
            {
                var lowestNumber = seasonNumbers.Min();
                var highestNumber = seasonNumbers.Max();
                seasonNumber = [highestNumber];
                var index = 0;
                ConsoleExt.WriteWithPretext($"Season Numbers: {lowestNumber}", ConsoleExt.OutputType.Info);
                for (var i = lowestNumber; i <= highestNumber; i++)
                {
                    seasonNumber[index] = i;
                    if (index != 0) Console.Write($", {seasonNumber[index]}");
                    index++;
                }

                Console.WriteLine();
                return seasonNumbers.ToArray();
            }
        }

        if (seasonNumber.Length == 0)
        {
            ConsoleExt.WriteLineWithPretext("Invalid Folder Name Input... No Folders have been Supplied.", ConsoleExt.OutputType.Error, new FormatException());
        }

        return [1];
    }

    //extracts the language from the folder name
    internal static void ExtractingLanguage(string inputFolderPath)
    {
        var fileName = new DirectoryInfo(inputFolderPath).Name;
        const string pattern = "Dual[- ]Audio";

        var match = Match(fileName, pattern);
        if (match.Success)
        {
            SetSubOrDub(Language.Dub);
            ConsoleExt.WriteLineWithPretext("Language is Dub", ConsoleExt.OutputType.Info);
            return;
        }

        var files = Directory.GetFiles(inputFolderPath);
        var languages = TrackLanguageFromMetadata(files[1]);

        if (languages.Contains("eng", StringComparer.OrdinalIgnoreCase) ||
            languages.Contains("ger", StringComparer.OrdinalIgnoreCase))
        {
            SetSubOrDub(Language.Dub);
            ConsoleExt.WriteLineWithPretext("Language is Dub", ConsoleExt.OutputType.Info);
            return;
        }

        if (languages.Contains("jpn", StringComparer.OrdinalIgnoreCase))
        {
            SetSubOrDub(Language.Sub);
            ConsoleExt.WriteLineWithPretext("Language is Sub", ConsoleExt.OutputType.Info);
            return;
        }

        if (languages.Contains("N/a", StringComparer.OrdinalIgnoreCase))
        {
            ConsoleExt.WriteLineWithPretext("File(s) is/are Corrupt!", ConsoleExt.OutputType.Error);
            return;
        }

        var arguments = fileName.Split(" ");

        foreach (var argument in arguments)
            switch (argument.ToLower())
            {
                case "sub":
                    SetSubOrDub(Language.Sub);
                    ConsoleExt.WriteLineWithPretext("Language is Sub", ConsoleExt.OutputType.Info);
                    break;
                case "dub":
                    SetSubOrDub(Language.Dub);
                    ConsoleExt.WriteLineWithPretext("Language is Dub", ConsoleExt.OutputType.Info);
                    break;
                default:
                    ConsoleExt.WriteLineWithPretext("No Language defining argument found.",
                        ConsoleExt.OutputType.Warning);
                    ConsoleExt.WriteLineWithPretext("Please enter the language of the anime (Sub or Dub)",
                        ConsoleExt.OutputType.Question);
                    Console.WriteLine("1. Sub");
                    Console.WriteLine("2. Dub");
                    var index = int.Parse(Console.ReadLine() ?? string.Empty);
                    switch (index)
                    {
                        case 1:
                            SetSubOrDub(Language.Sub);
                            break;
                        case 2:
                            SetSubOrDub(Language.Dub);
                            break;
                        default:
                            Console.WriteLine("Invalid input! Anime is defaulted to Dubbed!");
                            SetSubOrDub(Language.Dub);
                            break;
                    }

                    break;
            }
    }

    // ExtractingSeasonNumber()
    [GeneratedRegex(@"(?i)(Season|Seasons|S)\s*(\d+)\s*[+\-]+\s*(\d+)", RegexOptions.None, "en-US")]
    private static partial Regex MyRegex();
    [GeneratedRegex(@"(?i)\d+\s*(st|nd|rd|th)\s*(Season|Seasons|S)", RegexOptions.None, "en-US")]
    private static partial Regex MyRegex1();
    [GeneratedRegex(@"(?i)(Season|Seasons|S)\s*(\d+)", RegexOptions.None, "en-US")]
    private static partial Regex MyRegex2();
    [GeneratedRegex(@"(?<!['\w])[MCDLXVI]+(?![\w'])")]
    private static partial Regex MyRegex3();
    [GeneratedRegex("[+-]")]
    private static partial Regex MyRegex4();
    [GeneratedRegex(@"\d+")]
    private static partial Regex MyRegex5();
    
    // RemoveUnnecessaryNamePieces()
    [GeneratedRegex(@"\[.*?\]|\(.*?\)")]
    private static partial Regex MyRegex6();
    [GeneratedRegex("_")]
    private static partial Regex MyRegex7();
    [GeneratedRegex(@"(?i)(Season|Seasons|S)\s*(\d+)", RegexOptions.None, "en-US")]
    private static partial Regex MyRegex8();
    [GeneratedRegex(@"\d+(st|nd|rd|th)")]
    private static partial Regex MyRegex9();
    [GeneratedRegex(@"(?<!['\w])[MCDLXVI]+(?![\w'])")]
    private static partial Regex MyRegex10();
    [GeneratedRegex(@"\s{2,}.*$")]
    private static partial Regex MyRegex11();
    [GeneratedRegex(@"https:\/\/gogoanime\.\w+\/watch\/")]
    private static partial Regex MyRegex12();
    [GeneratedRegex(@"\/ep-\d+")]
    private static partial Regex MyRegex13();
    [GeneratedRegex("-")]
    private static partial Regex MyRegex14();
    [GeneratedRegex(@"\b\w+\s*$")]
    private static partial Regex MyRegex15();
    [GeneratedRegex(@"https:\/\/anix\.\w+\/anime\/")]
    private static partial Regex MyRegex16();
    [GeneratedRegex(@"(?i)(Part|Parts|P)\s*(\d+)\s*[+\-]+\s*(\d+)", RegexOptions.None, "en-US")]
    private static partial Regex MyRegex20();
    [GeneratedRegex(@"(?i)(Part|Parts|P)\s*(\d+)", RegexOptions.None, "en-US")]
    private static partial Regex MyRegex21();
}