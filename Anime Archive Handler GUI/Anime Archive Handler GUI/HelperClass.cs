using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anime_Archive_Handler_GUI;

using static InputStringHandler;


public static class HelperClass
{ 
    /// <summary>
    /// Converts a number into its ordinal representation (e.g., 1st, 2nd, 3rd, etc.).
    /// </summary>
    /// <param name="number">Number to be converted</param>
    /// <returns>Ordinal representation of the number</returns>
    /// <exception cref="ArgumentOutOfRangeException">Number must be a positive integer</exception>
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

    /// <summary>
    /// Converts a Roman numeral string to its integer value.
    /// </summary>
    /// <param name="roman">Roman numeral string</param>
    /// <returns>Integer value</returns>
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
    
    /// <summary>
    /// Checks if a given string is a valid URL.
    /// </summary>
    /// <param name="url">URL to check</param>
    /// <returns>Boolean indicating if the URL is valid</returns>
    public static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
    
    /// <summary>
    /// Gets the HTTP status code of a given URL asynchronously.
    /// </summary>
    /// <param name="url">URL to check</param>
    /// <returns>Task of the HTTP status code</returns>
    public static async Task<HttpStatusCode?> GetStatusCodeAsync(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            return null; // URL is not valid
        }

        try
        {
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10); // Set a reasonable timeout
            HttpResponseMessage response = await client.GetAsync(uriResult);
            return response.StatusCode;
        }
        catch (HttpRequestException)
        {
            // Handle network errors (e.g., DNS failure, refused connection, etc.)
            return null;
        }
        catch (TaskCanceledException)
        {
            // Handle request timeout
            return null;
        }
    }

    /// <summary>
    /// Checks with the user if the provided information is correct.
    /// </summary>
    /// <returns>User's answer</returns>
    /// <exception cref="InvalidOperationException">User's answer is invalid</exception>
    public static bool ManualInformationChecking()
    {
        ConsoleExt.WriteLineWithPretext("Is this Information Correct? (y/n)", ConsoleExt.OutputType.Question);
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
    
    /// <summary>
    /// Checks with the user if the provided information is correct.
    /// </summary>
    /// <param name="message">Message to be displayed</param>
    /// <returns>User's answer</returns>
    /// <exception cref="InvalidOperationException">User's answer is invalid</exception>
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

    /// <summary>
    /// Asks the user for a season number and extracts it from the input string.
    /// </summary>
    /// <param name="message">Message to be displayed</param>
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

    /// <summary>
    /// Creates all the required folders that don't get created when building the program but that need to be there
    /// </summary>
    public static void AddRequiredFolders()
    {
        var neededDirectories = JsonFileUtility.ReadNeededDirectories("./Databases/NeededDirectories.json");

        foreach (var neededDirectory in neededDirectories.Where(neededDirectory => neededDirectory.enabled))
        {
            Directory.CreateDirectory(neededDirectory.path);
        }
    }
    
    /// <summary>
    /// Creates a path-friendly date and time string.
    /// </summary>
    /// <returns>Path-friendly date and time string</returns>
    public static string PathFriendlyDateTime()
    {
        DateTime dateTime = DateTime.Now;

        var pattern1 = @"\.";
        var pattern2 = @"[\/\\:;]";

        var removedDots = Regex.Replace(dateTime.ToString(CultureInfo.CurrentCulture), pattern1, " ");
        
        var removeSlashesAndColons  = Regex.Replace(removedDots, pattern2, "-");
        
        return removeSlashesAndColons;
    }
    
    /// <summary>
    /// Extracts a specific property from a list of items using a selector function.
    /// </summary>
    /// <param name="items">Items to extract properties from</param>
    /// <param name="selector">Selector function</param>
    /// <typeparam name="T">Variable input type</typeparam>
    /// <typeparam name="TResult">Variable return type</typeparam>
    /// <returns></returns>
    public static List<TResult> ExtractProperty<T, TResult>(List<T> items, Func<T, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(selector);

        return items.Select(selector).ToList();
    }
    
    /// <summary>
    /// Levenshtein distance algorithm to calculate the distance between two strings.
    /// </summary>
    /// <param name="s">String 1</param>
    /// <param name="t">String 2</param>
    /// <returns>Integer distance</returns>
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