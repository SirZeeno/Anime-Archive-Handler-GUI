﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Anime_Archive_Handler_GUI;

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
        var pattern = @""; //this pattern needs to consist of the userInputString and any empty spaces that come before or after

        var removedWord = Regex.Replace(inputString, pattern, "");

        ConsoleExt.WriteLineWithPretext(removedWord.Trim(), ConsoleExt.OutputType.Info);

        return removedWord.Trim();
    }

    // Adds all the required folders that don't get created when building the program but that need to be there
    public static void AddRequiredFolders()
    {
        var neededDirectories = JsonFileUtility.ReadNeededDirectories("./Databases/NeededDirectories.json");

        foreach (var neededDirectory in neededDirectories.Where(neededDirectory => neededDirectory.enabled))
        {
            Directory.CreateDirectory(neededDirectory.path);
        }
    }
    
    public static string PathFriendlyDateTime()
    {
        DateTime dateTime = DateTime.Now;

        var pattern1 = @"\.";
        var pattern2 = @"[\/\\:;]";

        var removedDots = Regex.Replace(dateTime.ToString(CultureInfo.CurrentCulture), pattern1, " ");
        
        var removeSlashesAndColons  = Regex.Replace(removedDots, pattern2, "-");
        
        return removeSlashesAndColons;
    }
    
    public static List<TResult> ExtractProperty<T, TResult>(List<T> items, Func<T, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(selector);

        return items.Select(selector).ToList();
    }
    
    public static int LevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;

        // If one of the strings is empty
        if (n == 0) return m;
        if (m == 0) return n;

        // Create two work vectors of integer distances
        int[] v0 = new int[m + 1];
        int[] v1 = new int[m + 1];

        // Initialize v0 (the previous row of distances)
        // this row is A[0][i]: edit distance for an empty s
        // the distance is just the number of characters to delete from t
        for (int i = 0; i <= m; i++)
        {
            v0[i] = i;
        }

        for (int i = 0; i < n; i++)
        {
            // Calculate v1 (current row distances) from the previous row v0

            // First element of v1 is A[i+1][0]
            //   edit distance is delete (i+1) chars from s to match empty t
            v1[0] = i + 1;

            // Use formula to fill in the rest of the row
            for (int j = 0; j < m; j++)
            {
                int cost = (s[i] == t[j]) ? 0 : 1;
                v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
            }

            // Copy v1 (current row) to v0 (previous row) for next iteration
            for (int j = 0; j <= m; j++)
            {
                v0[j] = v1[j];
            }
        }

        return v1[m];
    }

}