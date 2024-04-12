using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JikanDotNet;
using JikanDotNet.Config;

namespace Anime_Archive_Handler_GUI.Desktop;

using static CommonSettings;

public static class JikanHandler
{
    private static int _id = 1;
    private static Anime? _anime;
    private static int _consecutiveNulls;

    //need to look into making the json database update every so often
    //need to look into hosting my own jikan API server
    //need to work on getting getAnimeRelations to work so i can link all relational animes together

    public static async Task Start()
    {
        var rateLimiter = new RateLimiter(40, 60000);

        while (true)
        {
            if (rateLimiter.Check())
            {
                //need to check if the specific malId is not contained in the database, if it is, it will skip it, if it isn't but there is a null on the line,
                //it will overwrite it, if there is no null and it actually doesnt exist it will add it, if it exists but some of the information changed it will overwrite it

                _anime = await GetAnime(_id);
                if (_anime != null)
                {
                    AnimeDb.Upsert(RemapToAnimeDto(_anime));
                }
                _id++;
                if (_anime == null)
                    _consecutiveNulls++;
                else
                    _consecutiveNulls = 0;
                if (_consecutiveNulls <= 500) break;
            }
            else
            {
                ConsoleExt.WriteLineWithPretext("Function skipped", ConsoleExt.OutputType.Warning);
            }

            await Task.Delay(1000);
        }
    }

    internal static async Task<ICollection<UserRecommendation>?> GetFeaturedAsync()
    {
        IJikan jikan = new Jikan(new JikanClientConfiguration { SuppressException = true });
        try
        {
            var animeResponse = await jikan.GetRecentAnimeRecommendationsAsync();
            return animeResponse.Data;
        }
        catch (Exception ex)
        {
            ConsoleExt.WriteLineWithPretext("Failed to get featured anime", ConsoleExt.OutputType.Error, ex);// Handle or log the exception as needed
            throw new Exception("Failed to get featured anime", ex);
            return null; // or return an empty collection as a fallback
        }
    }

    private static async Task<Anime?> GetAnime(int id)
    {
        ConsoleExt.WriteWithPretext("Getting Anime with ID: " + _id, ConsoleExt.OutputType.Info);
        IJikan jikan = new Jikan(new JikanClientConfiguration { SuppressException = true });
        BaseJikanResponse<Anime> animeResponseString = await jikan.GetAnimeAsync(id);
        PaginatedJikanResponse<ICollection<RelatedEntry>> relatedAnimeResponseString = await jikan.GetAnimeRelationsAsync(id); //need to integrate that into the system to get all related animes
        
        if (animeResponseString != null)
        {
            var anime = animeResponseString.Data;
            string? englishName = null;
            string? defaultName = null;
            if (anime is { Titles: not null })
            {
                foreach (var title in anime.Titles)
                    switch (title.Type.ToLower())
                    {
                        case "english":
                            englishName = title.Title;
                            break;
                        //default is mostly japanese in english characters
                        case "default":
                            defaultName = title.Title;
                            break;
                    }

                switch (englishName)
                {
                    case null when defaultName == null:
                        Console.Write(", English and Default Title Not Found!");
                        break;
                    case null:
                        Console.Write(", Name: " + defaultName);
                        break;
                    default:
                        Console.Write(", Name: " + englishName);
                        break;
                }
            }

            Console.WriteLine();
            //await Task.Delay(1000);
            return anime;
        }

        Console.Write(", Anime not found!");
        Console.WriteLine();
        return null;
    }

    private static AnimeDto RemapToAnimeDto(Anime anime)
    {
        var mapper = new MapperlyMaps();
        return mapper.AnimeDto(anime);
    }
}

internal class RateLimiter
{
    private readonly int _limit;
    private int _count;

    public RateLimiter(int limit, int interval)
    {
        _limit = limit;
        var timer = new Timer(ResetCounter!, null, interval, interval);
    }

    private void ResetCounter(object state)
    {
        _count = 0;
    }

    public bool Check()
    {
        if (_count >= _limit) return false;

        _count++;
        return true;
    }
}