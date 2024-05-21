using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet;

namespace Anime_Archive_Handler_GUI.Helpers;
using static ImageHelper;

public abstract class DailyFeatured
{
    private static readonly Lazy<Task<ICollection<UserRecommendation>?>> Featured = new(JikanHandler.GetFeaturedAsync);
    public static async Task<List<CarouselItem?>> PickDailyFeatured(int count = 5)
    {
        var featuredItems = await Featured.Value;
        ConsoleExt.WriteLineWithPretext("Picking Daily Featured", ConsoleExt.OutputType.Info);
    
        // Randomly pick 'count' items if the collection is larger than 'count', otherwise take what's available
        var randomItems = featuredItems.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();
    
        var tasks = randomItems.Select(async item =>
        {
            var firstEntry = item.Entries.FirstOrDefault();
            if (firstEntry?.Images.JPG.LargeImageUrl is null) return null;
            // Assuming LoadFromWeb returns a Task<Bitmap?>, ensure it's awaited once and reused
            var bitmap = await LoadFromWebTask(firstEntry.Images.JPG.LargeImageUrl);
            return new CarouselItem(bitmap, SkBitmapToAvaloniaBitmap(ApplyBlurEffect(ConvertToSkBitmap(bitmap))), firstEntry.Title, firstEntry.Name);
        });

        var carouselItems = await Task.WhenAll(tasks);
        return carouselItems.Where(item => item != null).ToList(); // Filter out any nulls, in case of failed loads
    }
}