using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Anime_Archive_Handler_GUI;

public static class ConsoleExt
{
    public enum OutputType
    {
        Error,
        Info,
        Warning,
        Question
    }

    /// <summary>
    /// Writes a line to the console with a pretext based on the output type.
    /// </summary>
    /// <param name="output">Output</param>
    /// <param name="outputType">Output type default is info</param>
    /// <param name="exception">Exception default is null</param>
    /// <typeparam name="T">Any type</typeparam>
    /// <returns>The length of the pretext</returns>
    public static int WriteLineWithPretext<T>(T output, OutputType outputType = OutputType.Info, Exception? exception = null)
    {
        var length1 = CurrentTime();
        var length2 = DetermineOutputType(outputType);
        if (output is IEnumerable enumerable && !(output is string))
        {
            Console.WriteLine(string.Join(", ", enumerable.Cast<object>()));
        }
        else
        {
            Console.WriteLine(output);
        }
        if (exception != null)
        {
            FileHandler.ErrorLogger(output!.ToString()!, exception);
        }
        return length1 + length2;
    }

    /// <summary>
    /// Writes a single line to the console with a pretext based on the output type.
    /// </summary>
    /// <param name="output">Output</param>
    /// <param name="outputType">Output type default is info</param>
    /// <param name="exception">Exception default is null</param>
    /// <typeparam name="T">Any type</typeparam>
    /// <returns>The length of the pretext</returns>
    public static int WriteWithPretext<T>(T output, OutputType outputType = OutputType.Info, Exception? exception = null)
    {
        var length1 = CurrentTime();
        var length2 = DetermineOutputType(outputType);
        if (output is IEnumerable enumerable && !(output is string))
        {
            Console.WriteLine(string.Join(", ", enumerable.Cast<object>()));
        }
        else
        {
            Console.Write(output);
        }
        if (exception != null)
        {
            FileHandler.ErrorLogger(output!.ToString()!, exception);
        }
        return length1 + length2;
    }

    /// <summary>
    /// Determines the output type and returns the length of the pretext.
    /// </summary>
    /// <param name="outputType">Output type</param>
    /// <returns>The length of the pretext</returns>
    private static int DetermineOutputType(OutputType outputType)
    {
        return outputType switch
        {
            OutputType.Error => ErrorType(),
            OutputType.Info => InfoType(),
            OutputType.Warning => WarningType(),
            OutputType.Question => QuestionType(),
            _ => 0
        };
    }

    private static int CurrentTime()
    {
        var dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"[{dateTime}] ");
        Console.ForegroundColor = oldColor;
        return dateTime.Length + 3;
    }

    private static int InfoType()
    {
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[Info] ");
        Console.ForegroundColor = oldColor;
        return 7;
    }

    private static int ErrorType()
    {
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("[Error] ");
        Console.ForegroundColor = oldColor;
        return 8;
    }

    private static int WarningType()
    {
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("[Warning] ");
        Console.ForegroundColor = oldColor;
        return 10;
    }

    private static int QuestionType()
    {
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("[Question] ");
        Console.ForegroundColor = oldColor;
        return 11;
    }
}