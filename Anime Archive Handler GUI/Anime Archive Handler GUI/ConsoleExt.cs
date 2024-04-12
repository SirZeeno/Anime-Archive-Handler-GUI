using System;
using System.Collections.Generic;

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

    public static int WriteLineWithPretext<T>(T output, OutputType outputType, Exception exception = null)
    {
        var length1 = CurrentTime();
        var length2 = DetermineOutputType(outputType);
        if (output is List<T> list)
        {
            Console.WriteLine(string.Join(", ", list));
        }
        if (output is T[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }
        else
        {
            Console.WriteLine(output);
        }
        /*
        if (exception != null)
        {
            FileHandler.ErrorLogger(output!.ToString()!, exception);
        }
        */
        return length1 + length2;
    }

    public static int WriteWithPretext<T>(T output, ConsoleExt.OutputType outputType, Exception? exception = null)
    {
        var length1 = CurrentTime();
        var length2 = DetermineOutputType(outputType);
        if (output is List<T> list)
        {
            Console.WriteLine(string.Join(", ", list));
        }
        if (output is T[] array)
        {
            Console.Write(string.Join(", ", array));
        }
        else
        {
            Console.Write(output);
        }
        /*
        if (exception != null)
        {
            FileHandler.ErrorLogger(output!.ToString()!, exception);
        }
        */
        return length1 + length2;
    }

    private static int DetermineOutputType(ConsoleExt.OutputType outputType)
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