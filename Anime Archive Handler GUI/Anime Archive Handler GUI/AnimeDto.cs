using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Avalonia.Media.Imaging;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Anime_Archive_Handler_GUI;

public class AnimeDto
{
    [Key]
    public long? MalId { get; set; }
    [MaxLength(200)]
    public string Url { get; set; }
    public ImagesSetDto Images { get; set; }
    public AnimeTrailerDto Trailer { get; set; }
    [MaxLength(150)]
    public string? Title { get; set; }
    [MaxLength(150)]
    public string? TitleEnglish { get; set; }
    [MaxLength(150)]
    public string? TitleJapanese { get; set; }
    public ICollection<TitleSynonymDto> TitleSynonyms { get; set; }
    public ICollection<TitleEntryDto> Titles { get; set; }
    [MaxLength(100)]
    public string? Type { get; set; }
    [MaxLength(100)]
    public string Source { get; set; }
    public int? Episodes { get; set; }
    [MaxLength(100)]
    public string Status { get; set; }
    public bool Airing { get; set; }
    public TimePeriodDto Aired { get; set; }
    [MaxLength(100)]
    public string Duration { get; set; }
    [MaxLength(100)]
    public string? Rating { get; set; }
    public double? Score { get; set; }
    public int? ScoredBy { get; set; }
    public int? Rank { get; set; }
    public int? Popularity { get; set; }
    public int? Members { get; set; }
    public int? Favorites { get; set; }
    public string? Synopsis { get; set; }
    public string? Background { get; set; }
    public SeasonDto? Season { get; set; }
    public int? Year { get; set; }
    public AnimeBroadcastDto Broadcast { get; set; }
    public ICollection<ProducersDto> Producers { get; set; }
    public ICollection<LicensorsDto> Licensors { get; set; }
    public ICollection<StudiosDto> Studios { get; set; }
    public ICollection<GenresDto> Genres { get; set; }
    public ICollection<ExplicitGenresDto> ExplicitGenres { get; set; }
    public ICollection<ThemesDto> Themes { get; set; }
    public ICollection<DemographicsDto> Demographics { get; set; }
    public bool Approved { get; set; }
}

public class MalUrlDto
{
    [Key]
    public long Id { get; set; }
    [MaxLength(50)]
    public string? Type { get; set; }
    [MaxLength(200)]
    public string? Url { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }

    public long? AnimeId { get; set; } // Foreign key
    public List<AnimeDto>? Anime { get; init; }
    public override string ToString() => this.Name ?? base.ToString();
}

public class ProducersDto : MalUrlDto{}

public class LicensorsDto : MalUrlDto{}

public class StudiosDto : MalUrlDto{}

public class GenresDto : MalUrlDto{}

public class ExplicitGenresDto : MalUrlDto{}

public class ThemesDto : MalUrlDto{}

public class DemographicsDto : MalUrlDto{}

public enum SeasonDto
{
    [Description("spring")] Spring,
    [Description("summer")] Summer,
    [Description("fall")] Fall,
    [Description("winter")] Winter,
}

public class TimePeriod
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}

public class TitleEntryDto
{
    [Key]
    public int Id { get; set; } // Add a key to this class as EF requires one
    [MaxLength(50)]
    public string Type { get; set; }
    [MaxLength(150)]
    public string? Title { get; set; }
    
    public long AnimeId { get; set; } // Foreign key
    public AnimeDto Anime { get; set; }
}

public class TitleFtsDto
{
    [MaxLength(50)]
    public string Type { get; set; }
    [MaxLength(150)]
    public string? Title { get; set; }
    public long AnimeId { get; set; } // Foreign key
}

public class AnimeTrailerDto
{
    [Key]
    public int Id { get; set; } // Add a key to this class as EF requires one
    [MaxLength(200)]
    public string? YoutubeId { get; set; }
    [MaxLength(200)]
    public string? Url { get; set; }
    [MaxLength(200)]
    public string? EmbedUrl { get; set; }
    public ImageDto Image { get; set; }
    public long AnimeId { get; set; } // Foreign key
    public AnimeDto Anime { get; set; }
}

public class ImagesSetDto
{
    [Key]
    public int Id { get; set; } // Add a key to this class as EF requires one
    public int JPGId { get; set; }
    public ImageDto JPG { get; set; }
    
    public int WebPId { get; set; }
    public ImageDto WebP { get; set; }
}

public class ImageDto
{
    [Key]
    public int Id { get; set; } // Add a key to this class as EF requires one
    [MaxLength(200)]
    public string? ImageUrl { get; set; }
    [MaxLength(200)]
    public string? SmallImageUrl { get; set; }
    [MaxLength(200)]
    public string? MediumImageUrl { get; set; }
    [MaxLength(200)]
    public string? LargeImageUrl { get; set; }
    [MaxLength(200)]
    public string? MaximumImageUrl { get; set; }
}

public class AnimeBroadcastDto
{
    [Key]
    public int Id { get; set; } // Primary Key
    [MaxLength(50)]
    public string? Day { get; set; }
    [MaxLength(50)]
    public string? Time { get; set; }
    [MaxLength(50)]
    public string? Timezone { get; set; }
    [MaxLength(100)]
    public string? String { get; set; }
}

public class TitleSynonymDto
{
    [Key]
    public int Id { get; set; } // Primary Key
    public string? Synonym { get; set; }
    public long AnimeId { get; set; } // Foreign key
    public AnimeDto Anime { get; set; }
}

public class TimePeriodDto
{
    [Key]
    public int Id { get; set; } // Primary Key
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public long AnimeId { get; set; } // Foreign key
    public AnimeDto Anime { get; set; }
}

public class AnimeImageSetBitmap()
{
    [Key]
    public long? MalId { get; set; } // Primary Key
    public int JPGId { get; set; }
    public AnimeImageBitmap? JPG { get; set; }
    public int WebPId { get; set; }
    public AnimeImageBitmap? WebP { get; set; }
}

public class AnimeImageBitmap()
{
    [Key]
    public int Id { get; set; } // Primary Key
    public byte[]? ImageBitmap { get; set; }
    public byte[]? SmallImageBitmap { get; set; }
    public byte[]? MediumImageBitmap { get; set; }
    public byte[]? LargeImageBitmap { get; set; }
    public byte[]? MaximumImageBitmap { get; set; }
    
    public long? AnimeImageSetBitmapId { get; set; } // Foreign key
    public AnimeImageSetBitmap AnimeImageSetBitmap { get; set; }
}