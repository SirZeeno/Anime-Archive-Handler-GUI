using System;
using System.Collections.Generic;

namespace Anime_Archive_Handler_GUI;

public static class Extensions
{
    /// <summary>
    /// Splits a string into chunks of a specified size.
    /// </summary>
    /// <param name="str">String to split</param>
    /// <param name="chunkSize">Chunk size</param>
    /// <returns>IEnumerable of chunks that the string was split into</returns>
    /// <exception cref="ArgumentNullException">String is null</exception>
    /// <exception cref="ArgumentException">Chunk size is less than or equal to zero</exception>
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