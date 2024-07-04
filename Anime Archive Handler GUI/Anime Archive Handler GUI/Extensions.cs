using System;
using System.Collections.Generic;

namespace Anime_Archive_Handler_GUI;

public static class Extensions
{
    public static IEnumerable<string> SplitIntoChunks(this string str, int chunkSize)
    {
        if (str == null)
            throw new ArgumentNullException(nameof(str));

        if (chunkSize <= 0)
            throw new ArgumentException("Chunk size must be greater than zero.", nameof(chunkSize));

        for (int i = 0; i < str.Length; i += chunkSize)
        {
            yield return str.Substring(i, Math.Min(chunkSize, str.Length - i));
        }
    }
}