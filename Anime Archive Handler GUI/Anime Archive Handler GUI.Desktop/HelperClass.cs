using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Anime_Archive_Handler_GUI.Desktop;

using static InputStringHandler;


public static class HelperClass
{ 
    //converts input number into ordinal number
    public static string ToOrdinal(int number)
    {
        if (number <= 0)
            throw new ArgumentOutOfRangeException(nameof(number), "The number must be a positive integer.");

        switch (number % 100)
        {
            case 11:
            case 12:
            case 13:
                return number + "th";
        }

        return (number % 10) switch
        {
            1 => number + "st",
            2 => number + "nd",
            3 => number + "rd",
            _ => number + "th"
        };
    }

    public static int ConvertRomanToNumber(string roman)
    {
        var romanValues = new Dictionary<char, int>
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        var result = 0;
        var previousValue = 0;

        for (var i = roman.Length - 1; i >= 0; i--)
        {
            var currentValue = romanValues[roman[i]];

            if (currentValue < previousValue)
                result -= currentValue;
            else
                result += currentValue;

            previousValue = currentValue;
        }

        return result;
    }

    public static int ConvertMixedStringToNumber(string input)
    {
        var romanValues = new Dictionary<char, int>
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        var result = 0;
        var currentRoman = string.Empty;

        foreach (var c in input)
            if (romanValues.ContainsKey(c))
            {
                currentRoman += c;
            }
            else if (currentRoman != string.Empty)
            {
                result += ConvertRomanToNumber(currentRoman);
                currentRoman = string.Empty;
            }

        // Convert the last extracted Roman numeral if any
        if (currentRoman != string.Empty) result += ConvertRomanToNumber(currentRoman);

        return result;
    }

    public static bool ManualInformationChecking()
    {
        ConsoleExt.WriteLineWithPretext("Is this Information Correct? (y/n)", ConsoleExt.OutputType.Question);
        var answer = Console.ReadLine()?.ToLower();
        switch (answer?.ToLower())
        {
            case "y":
            case "yes":
                return true;
            case "n":
            case "no":
                return false;
            default:
                ConsoleExt.WriteLineWithPretext("Answer Provided is either null or Indeterminable!",
                    ConsoleExt.OutputType.Error);

                throw new InvalidOperationException();
        }
    }
    
    public static bool ManualInformationChecking(string message)
    {
        ConsoleExt.WriteLineWithPretext($"{message} (y/n)", ConsoleExt.OutputType.Question);
        var answer = Console.ReadLine()?.ToLower();
        switch (answer?.ToLower())
        {
            case "y":
            case "yes":
                return true; // returns true if yes
            case "n":
            case "no":
                return false; // returns false if no
            default:
                ConsoleExt.WriteLineWithPretext("Answer Provided is either null or Indeterminable!",
                    ConsoleExt.OutputType.Error);

                throw new InvalidOperationException();
        }
    }

    public static void ManualSeasonNumber(string message)
    {
        ConsoleExt.WriteLineWithPretext($"{message} (Numbers/Symbols Only!)", ConsoleExt.OutputType.Question);
        ConsoleExt.WriteLineWithPretext("Warning: numbers only in forms of 1,2,3 or 1+2+3 or 1-3", ConsoleExt.OutputType.Warning);
        var question = Regex.Escape("Season Number(s): ");
        var answer = Console.ReadLine();
        if (answer == null) return;
        var cutInputString = Regex.Replace(answer, question, "Season ");
        ExtractingSeasonNumber(cutInputString);
    }

    
    //incomplete
    public static string ManualStringRemoval(string? userInputString, string inputString)
    {
        var pattern =
            @""; //this pattern needs to consist of the userInputString and any empty spaces that come before or after

        var removedWord = Regex.Replace(inputString, pattern, "");

        ConsoleExt.WriteLineWithPretext(removedWord.Trim(), ConsoleExt.OutputType.Info);

        return removedWord.Trim();
    }

    // Adds all the required folders that dont get created when building the program but that need to be there
    public static void AddRequiredFolders()
    {
        if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Databases/Downloads")))
        {
            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Databases/Downloads"));
        }

        if (Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Logs/Errors")))
        {
            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Logs/Errors"));
        }
    }

    // Todo: Implement this function to create a path friendly date time
    public static void PathFriendlyDateTime()
    {
        
    }
}