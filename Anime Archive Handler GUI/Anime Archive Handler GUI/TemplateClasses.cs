using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
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

public class AnimeCarousel(ObservableCollection<CarouselItem> items)
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