using System;
using System.Collections.Generic;

public class AnimeDto
{
    public long MalId { get; set; } // Primary Key
    public string Url { get; set; }
    public ImagesSetDto Images { get; set; }
    public AnimeTrailerDto Trailer { get; set; }
    public ICollection<TitleEntryDto> Titles { get; set; }
    public int AnimeSeason { get; set; }
    public int? AnimePart { get; set; }
    public string Type { get; set; }
    public string Source { get; set; }
    public int? Episodes { get; set; }
    public string Status { get; set; }
    public bool Airing { get; set; }
    public TimePeriodDto Aired { get; set; }
    public string Duration { get; set; }
    public string Rating { get; set; }
    public double? Score { get; set; }
    public int? ScoredBy { get; set; }
    public int? Rank { get; set; }
    public int? Popularity { get; set; }
    public int? Members { get; set; }
    public int? Favorites { get; set; }
    public string Synopsis { get; set; }
    public string Background { get; set; }
    public SeasonDto? Season { get; set; }
    public int? Year { get; set; }
    public AnimeBroadcastDto Broadcast { get; set; }
    public ICollection<AnimeProducer> Producers { get; set; }
    public ICollection<AnimeLicensor> Licensors { get; set; }
    public ICollection<AnimeStudio> Studios { get; set; }
    public ICollection<AnimeGenre> Genres { get; set; }
    public ICollection<AnimeExplicitGenre> ExplicitGenres { get; set; }
    public ICollection<AnimeTheme> Themes { get; set; }
    public ICollection<AnimeDemographic> Demographics { get; set; }
    public bool Approved { get; set; }
}

public class AnimeBroadcastDto
{
    public long Id { get; set; } // Primary Key
    public string Day { get; set; }
    public string Time { get; set; }
    public string Timezone { get; set; }
    public string String { get; set; }
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; } // Navigation Property
}

public class AnimeTrailerDto
{
    public long Id { get; set; } // Primary Key
    public string YoutubeId { get; set; }
    public string Url { get; set; }
    public string EmbedUrl { get; set; }
    public ImageDto Image { get; set; }
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; } // Navigation Property
}

public class ImageDto
{
    public long Id { get; set; } // Primary Key
    public string ImageUrl { get; set; }
    public string SmallImageUrl { get; set; }
    public string MediumImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
    public string MaximumImageUrl { get; set; }
    public long? ParentId { get; set; } // Foreign Key
    public AnimeTrailerDto AnimeTrailer { get; set; } // Navigation Property
    public long? ImagesSetJpgId { get; set; } // Foreign Key
    public long? ImagesSetWebPId { get; set; } // Foreign Key
    public ImagesSetDto ImagesSetJpg { get; set; } // Navigation Property
    public ImagesSetDto ImagesSetWebP { get; set; } // Navigation Property
}

public class MalUrlDto
{
    public long Id { get; set; } // Primary Key
    public long MalId { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; } // Navigation Property
}

public class TitleEntryDto
{
    public long Id { get; set; } // Primary Key
    public string Type { get; set; }
    public string Title { get; set; }
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; } // Navigation Property
}

public class ImagesSetDto
{
    public long Id { get; set; } // Primary Key
    public ImageDto JPG { get; set; }
    public ImageDto WebP { get; set; }
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; } // Navigation Property
}

public class TimePeriodDto
{
    public long Id { get; set; } // Primary Key
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; } // Navigation Property
}

public enum SeasonDto
{
    Spring,
    Summer,
    Fall,
    Winter,
}

public class AnimeProducer
{
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; }
    public long MalUrlId { get; set; } // Foreign Key
    public MalUrlDto Producer { get; set; }
}

public class AnimeLicensor
{
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; }
    public long MalUrlId { get; set; } // Foreign Key
    public MalUrlDto Licensor { get; set; }
}

public class AnimeStudio
{
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; }
    public long MalUrlId { get; set; } // Foreign Key
    public MalUrlDto Studio { get; set; }
}

public class AnimeGenre
{
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; }
    public long MalUrlId { get; set; } // Foreign Key
    public MalUrlDto Genre { get; set; }
}

public class AnimeExplicitGenre
{
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; }
    public long MalUrlId { get; set; } // Foreign Key
    public MalUrlDto ExplicitGenre { get; set; }
}

public class AnimeTheme
{
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; }
    public long MalUrlId { get; set; } // Foreign Key
    public MalUrlDto Theme { get; set; }
}

public class AnimeDemographic
{
    public long AnimeDtoId { get; set; } // Foreign Key
    public AnimeDto Anime { get; set; }
    public long MalUrlId { get; set; } // Foreign Key
    public MalUrlDto Demographic { get; set; }
}
