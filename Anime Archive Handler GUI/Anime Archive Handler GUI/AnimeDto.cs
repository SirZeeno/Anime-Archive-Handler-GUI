using System;
using System.Collections.Generic;
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace Anime_Archive_Handler_GUI;

#nullable disable
public class AnimeDto // I need to recreate the entirety of the anime class in this class including its subclass to make it mappable by SQLite
{
    /// <summary>ID associated with MyAnimeList.</summary>
    public long? MalId { get; set; }

    /// <summary>Anime's canonical link.</summary>
    public string Url { get; set; }

    /// <summary>Anime's images in various formats.</summary>
    public ImagesSetDto Images { get; set; }

    /// <summary>Anime's trailer.</summary>
    public AnimeTrailerDto Trailer { get; set; }

    /// <summary>Anime's multiple titles (if any).</summary>
    public ICollection<TitleEntryDto> Titles { get; set; }
    
    /// <summary>Season of anime.</summary>
    public int AnimeSeason { get; init; }
    
    /// <summary>Part of anime if applicable.</summary>
    public int? AnimePart { get; init; }

    /// <summary>Anime type (e. g. "TV", "Movie").</summary>
    public string Type { get; set; }

    /// <summary>Anime source (e .g. "Manga" or "Original").</summary>
    public string Source { get; set; }

    /// <summary>Anime's episode count.</summary>
    public int? Episodes { get; set; }

    /// <summary>Anime's airing status (e.g. "Currently Airing").</summary>
    public string Status { get; set; }

    /// <summary>Is anime currently airing?</summary>
    public bool Airing { get; set; }

    /// <summary>
    /// Associative keys "from" and "to" which are alternative versions of AiredString in ISO8601 format.
    /// </summary>
    public TimePeriodDto Aired { get; set; }

    /// <summary>Anime's duration per episode.</summary>
    public string Duration { get; set; }

    /// <summary>Anime's age rating.</summary>
    public string Rating { get; set; }

    /// <summary>Anime's score on MyAnimeList up to 2 decimal places.</summary>
    public double? Score { get; set; }

    /// <summary>The Number of people the anime has been scored by.</summary>
    public int? ScoredBy { get; set; }

    /// <summary>Anime rank on MyAnimeList (score).</summary>
    public int? Rank { get; set; }

    /// <summary>Anime popularity rank on MyAnimeList.</summary>
    public int? Popularity { get; set; }

    /// <summary>Anime members count on MyAnimeList.</summary>
    public int? Members { get; set; }

    /// <summary>Anime favourite count on MyAnimeList.</summary>
    public int? Favorites { get; set; }

    /// <summary>Anime's synopsis.</summary>
    public string Synopsis { get; set; }

    /// <summary>Anime's background info.</summary>
    public string Background { get; set; }

    /// <summary>Season of the year the anime premiered.</summary>
    public SeasonDto? Season { get; set; }

    /// <summary>Year the anime premiered.</summary>
    public int? Year { get; set; }

    /// <summary>Anime broadcast day and timings (usually JST).</summary>
    public AnimeBroadcastDto Broadcast { get; set; }

    /// <summary>
    /// Anime's producers numerically indexed with array values.
    /// </summary>
    public ICollection<AnimeProducer> Producers { get; set; }

    /// <summary>
    /// Anime's licensors numerically indexed with array values.
    /// </summary>
    public ICollection<AnimeLicensor> Licensors { get; set; }

    /// <summary>
    /// Anime's studio(s) numerically indexed with array values.
    /// </summary>
    public ICollection<AnimeStudio> Studios { get; set; }

    /// <summary>Anime's genres numerically indexed with array values.</summary>
    public ICollection<AnimeGenre> Genres { get; set; }

    /// <summary>Explicit genres</summary>
    public ICollection<AnimeExplicitGenre> ExplicitGenres { get; set; }

    /// <summary>Anime's themes</summary>
    public ICollection<AnimeTheme> Themes { get; set; }

    /// <summary>Anime's demographics</summary>
    public ICollection<AnimeDemographic> Demographics { get; set; }

    /// <summary>
    /// If Approved is false, then this means the entry is still pending review on MAL.
    /// </summary>
    public bool Approved { get; set; }
}

public class AnimeBroadcastDto
{
    /// <summary>Database ID</summary>
    public long Id { get; init; }
    
    /// <summary>Day of the week</summary>
    public string Day { get; set; }

    /// <summary>Time in 24-hour format</summary>
    public string Time { get; set; }

    /// <summary>Timezone (Tz Database format https://en.wikipedia.org/wiki/List_of_tz_database_time_zones)</summary>
    public string Timezone { get; set; }

    /// <summary>Raw parsed broadcast string</summary>
    public string String { get; set; }
    
    /// <summary>Foreign keys</summary>
    public long AnimeDtoId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public AnimeDto Anime { get; init; }
}

public class AnimeTrailerDto
{
    /// <summary>Database ID</summary>
    public long Id { get; init; }
    
    /// <summary>ID associated with YouTube.</summary>
    public string YoutubeId { get; set; }

    /// <summary>Url to the video.</summary>
    public string Url { get; set; }

    /// <summary>Embed url to the video.</summary>
    public string EmbedUrl { get; set; }

    /// <summary>Image related to the trailer in various resolutions.</summary>
    public ImageDto Image { get; set; }
    
    /// <summary>Foreign keys</summary>
    public long AnimeDtoId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public AnimeDto Anime { get; init; }
}

public class ImageDto
{
    /// <summary>Database ID</summary>
    public long Id { get; init; }
    
    /// <summary>Url to the default version of the image.</summary>
    public string ImageUrl { get; set; }

    /// <summary>Url to the small version of the image.</summary>
    public string SmallImageUrl { get; set; }

    /// <summary>Url to the medium version of the image.</summary>
    public string MediumImageUrl { get; set; }

    /// <summary>Url to the large version of the image.</summary>
    public string LargeImageUrl { get; set; }

    /// <summary>Url to the version of image with the biggest resolution.</summary>
    public string MaximumImageUrl { get; set; }
    
    /// <summary>Foreign keys</summary>
    public long ParentId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public AnimeTrailerDto AnimeTrailer { get; init; }
    
    public long? ImagesSetJpgId { get; init; } // Foreign Key to ImagesSetDto for JPG
    public long? ImagesSetWebPId { get; init; } // Foreign Key to ImagesSetDto for WebP
    public ImagesSetDto ImagesSetJpg { get; init; } // Navigation Property for JPG
    public ImagesSetDto ImagesSetWebP { get; init; } // Navigation Property for WebP
}

public class MalUrlDto
{
    /// <summary>Database ID</summary>
    public long Id { get; init; }
    
    /// <summary>ID associated with MyAnimeList.</summary>
    public long MalId { get; init; }

    /// <summary>Type of resource</summary>
    public string Type { get; init; }

    /// <summary>Url to sub item main page.</summary>
    public string Url { get; init; }

    /// <summary>Title/Name of the item</summary>
    public string Name { get; init; }
    
    /// <summary>Foreign keys</summary>
    public long AnimeDtoId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public AnimeDto Anime { get; init; }

    /// <summary>Overriden ToString method.</summary>
    /// <returns>Title if not null, base method elsewhere.</returns>
    public override string ToString() => Name ?? base.ToString();
}

public class TitleEntryDto
{
    /// <summary>Database ID</summary>
    public long Id { get; init; }
    
    /// <summary>Type of title (usually the language).</summary>
    public string Type { get; set; }

    /// <summary>Value of the Title.</summary>
    public string Title { get; set; }
    
    /// <summary>Foreign keys</summary>
    public long AnimeDtoId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public AnimeDto Anime { get; init; }
}

public class ImagesSetDto
{
    /// <summary>Database ID</summary>
    public long Id { get; init; }
    
    public long? JpgId { get; init; } // Foreign Key to ImageDto for JPG
    public long? WebPId { get; init; } // Foreign Key to ImageDto for WebP
    
    /// <summary>Images in JPG format.</summary>
    public ImageDto Jpg { get; set; }

    /// <summary>Images in webp format.</summary>
    public ImageDto WebP { get; set; }
    
    /// <summary>Foreign keys</summary>
    public long AnimeDtoId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public ImageDto Image { get; init; }
    
    /// <summary>Foreign keys</summary>
    public long ImageDtoId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public AnimeDto Anime { get; init; }
}

public class TimePeriodDto
{
    /// <summary>Database ID</summary>
    public long Id { get; init; }
    
    /// <summary>Start date.</summary>
    public DateTime? From { get; set; }

    /// <summary>End date.</summary>
    public DateTime? To { get; set; }
    
    /// <summary>Foreign keys</summary>
    public long AnimeDtoId { get; init; }
    
    /// <summary>Navigation Property</summary>
    public AnimeDto Anime { get; init; }
}

public enum SeasonDto
{
    /// <summary>Spring season.</summary>
    Spring,
    /// <summary>Summer season.</summary>
    Summer,
    /// <summary>Fall season.</summary>
    Fall,
    /// <summary>Winter season.</summary>
    Winter,
}

public class AnimeProducer
{
    public long AnimeDtoId { get; init; } // Foreign Key
    public AnimeDto Anime { get; init; }
    public long MalUrlId { get; init; } // Foreign Key
    public MalUrlDto Producer { get; init; }
}

public class AnimeLicensor
{
    public long AnimeDtoId { get; init; } // Foreign Key
    public AnimeDto Anime { get; init; }
    public long MalUrlId { get; init; } // Foreign Key
    public MalUrlDto Licensor { get; init; }
}

public class AnimeStudio
{
    public long AnimeDtoId { get; init; } // Foreign Key
    public AnimeDto Anime { get; init; }
    public long MalUrlId { get; init; } // Foreign Key
    public MalUrlDto Studio { get; init; }
}

public class AnimeGenre
{
    public long AnimeDtoId { get; init; } // Foreign Key
    public AnimeDto Anime { get; init; }
    public long MalUrlId { get; init; } // Foreign Key
    public MalUrlDto Genre { get; init; }
}

public class AnimeExplicitGenre
{
    public long AnimeDtoId { get; init; } // Foreign Key
    public AnimeDto Anime { get; init; }
    public long MalUrlId { get; init; } // Foreign Key
    public MalUrlDto ExplicitGenre { get; init; }
}

public class AnimeTheme
{
    public long AnimeDtoId { get; init; } // Foreign Key
    public AnimeDto Anime { get; init; }
    public long MalUrlId { get; init; } // Foreign Key
    public MalUrlDto Theme { get; init; }
}

public class AnimeDemographic
{
    public long AnimeDtoId { get; init; } // Foreign Key
    public AnimeDto Anime { get; init; }
    public long MalUrlId { get; init; } // Foreign Key
    public MalUrlDto Demographic { get; init; }
}