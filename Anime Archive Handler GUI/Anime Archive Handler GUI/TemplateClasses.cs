using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Anime_Archive_Handler_GUI.Helpers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using JikanDotNet;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace Anime_Archive_Handler_GUI;

public class CarouselItem(Bitmap imagePath, Bitmap bgImagePath, string title, string description)
{
    public Bitmap ImagePath { get; set; } = imagePath;
    public Bitmap BgImagePath { get; set; } = bgImagePath;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
}

public class AnimeCarousel(ObservableCollection<CarouselItem>? items)
{
    public ObservableCollection<CarouselItem>? Items { get; set; } = items;
    
    public void Next(Carousel source)
    {
        if (source.SelectedItem == source.Items[^1])
        {
            source.SelectedItem = source.Items[0];
        }
        else
        {
            source.Next();
        }
    }

    public void Previous(Carousel source) 
    {
        if (source.SelectedItem == source.Items.First())
        {
            source.SelectedItem = source.Items.Last();
        }
        else
        {
            source.Previous();
        }
    }
}

public class AnimeDisplayItem(string animeImageUrl, string animeName, int subEpisodeCount, int dubEpisodeCount, int overallEpisodeCount, Language subOrDub = default, int paddingThickness = 10, int imageMaxWidth = 225, int imageMaxHeight = 335)
{
    // Main Information
    public Bitmap? AnimeImage { get; } = ImageHelper.LoadFromWeb(animeImageUrl);
    public string AnimeName { get; } = animeName;
    public int SubEpisodeCount { get; } = subEpisodeCount;
    public int DubEpisodeCount { get; } = dubEpisodeCount;
    public int OverallEpisodeCount { get; } = overallEpisodeCount;
    
    // Functional Settings
    public Language SubOrDub { get; } = subOrDub; //ToDo: add functionality for dub and sub
    
    // Cosmetic Setting
    public int PaddingThickness { get; } = paddingThickness;
    public int ImageMaxWidth { get; } = imageMaxWidth;
    public int ImageMaxHeight { get; } = imageMaxHeight;
    public LinearGradientBrush LinearGradientBrush { get; } = new()
    {
        StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
        EndPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
        GradientStops =
        [
            new GradientStop(Color.FromArgb(0, 0, 0, 0), 0), // Fully transparent at the top
            new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.6), // Fully transparent at the top
            new GradientStop(Color.FromArgb(150, 32, 32, 32), 0.75),
            new GradientStop(Color.FromArgb(225, 32, 32, 32), 0.85),
            new GradientStop(Color.FromArgb(235, 32, 32, 32), 0.9),
            new GradientStop(Color.FromArgb(255, 32, 32, 32), 0.98) // Replace with your border's background color, fully opaque at the bottom
        ]
    };
}

public class AnimeImportDisplayItem(string animeTitle, ObservableCollection<AnimeDisplayItem>? animeSearchResults)
{
    public string AnimeTitle { get; set; } = animeTitle;
    public ObservableCollection<AnimeDisplayItem>? AnimeSearchResults { get; set; } = animeSearchResults;
}

public class ImportSettings(string? selectedPath, bool hasMultipleInOneFolder, bool hasSeasonFolders, bool isOva, bool isMovie, ImportType importType)
{
    public string? SelectedPath { get; } = selectedPath;
    public bool HasMultipleInOneFolder { get; } = hasMultipleInOneFolder;
    public bool HasSeasonFolders { get; } = hasSeasonFolders;
    public bool IsOva { get; } = isOva;
    public bool IsMovie { get; } = isMovie;
    public ImportType ImportType { get; } = importType;
}

public enum ImportType
{
    Anime,
    Manga,
    Hentai
}

public abstract class Animetosho
{
    public int? id { get; set; }
    public int? tosho_id { get; set; }
    public int? nyaa_id { get; set; }
    public int? anidex_id { get; set; }
    public string name { get; set; }
    public string link { get; set; }
    public string magnet { get; set; }
    public int? cat { get; set; }
    public string website { get; set; }
    public double? totalsize { get; set; }
    public int? date_posted { get; set; }
    public string comment { get; set; }
    public int? date_added { get; set; }
    public int? date_completed { get; set; }
    public string torrentname { get; set; }
    public int? torrentfiles { get; set; }
    public int? stored_nzb { get; set; }
    public int? stored_torrent { get; set; }
    public string tosho_uhash { get; set; }
    public int? tosho_uauth { get; set; }
    public string tosho_uname { get; set; }
    public string nyaa_info { get; set; }
    public int? nyaa_class { get; set; }
    public string nyaa_cat { get; set; }
    public string anidex_info { get; set; }
    public int? anidex_cat { get; set; }
    public int? anidex_lang { get; set; }
    public int? anidex_labels { get; set; }
    public string btih { get; set; }
    public string btih_sha256 { get; set; }
    public bool? isdupe { get; set; }
    public int? deleted { get; set; }
    public int? date_updated { get; set; }
    public int? aid { get; set; }
    public int? eid { get; set; }
    public int? fid { get; set; }
    public string gids { get; set; }
    public int? resolveapproved { get; set; }
    public int? main_fileid { get; set; }
    public string srcurl { get; set; }
    public string srcurltype { get; set; }
    public string srctitle { get; set; }
    public int? status { get; set; }
}
public class AnimeDto : Anime
{
    public int AnimeSeason { get; set; }
    public int AnimePart { get; set; }
}

public class TitleEntryDb : TitleEntry
{
    public long? MalId { get; init; }
}
    
public class NeededDirectories
{
    public string path { get; set; }
    public bool enabled { get; set; }
}

public class NHentaiMetaData
{
    public int id { get; set; }
    public int nhentaiId { get; set; }
    public string title { get; set; }
    public string subtitle { get; set; }
    public string upload_date { get; set; }
    public string parody { get; set; }
    public string[] character { get; set; }
    public string[] tag { get; set; }
    public string[] artist { get; set; }
    public string[] group { get; set; }
    public string[] language { get; set; }
    public string[] category { get; set; }
    public string URL { get; set; }
    public int Pages { get; set; }
    
    public string folderLocation { get; set; }
}

public abstract class Languages
{
    public string name { get; set; }
    public string short_form { get; set; }
    public LanguageForm form { get; set; }
    public bool is_active { get; set; }
}

public abstract class LanguageForm
{
    public bool sub { get; set; }
    public bool dub { get; set; }
}