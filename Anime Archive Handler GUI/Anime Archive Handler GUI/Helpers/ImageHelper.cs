using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SkiaSharp;

namespace Anime_Archive_Handler_GUI.Helpers;

public static class ImageHelper
{
    private static readonly SKPaint BlurPaint = new()
    {
        IsAntialias = true,
        FilterQuality = SKFilterQuality.High,
        ImageFilter = SKImageFilter.CreateBlur(10, 10) // Example blur radius, adjust as needed
    };
    public static Bitmap LoadFromResource(string resourcePath)
    {
        Uri resourceUri;
        if (!resourcePath.StartsWith("avares://"))
        {
            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            resourceUri = new Uri($"avares://{assemblyName}/{resourcePath.TrimStart('/')}");
        }
        else
        {
            resourceUri = new Uri(resourcePath);
        }
        return new Bitmap(AssetLoader.Open(resourceUri));
    }

    public static Bitmap LoadFromFile(string filePath)
    {
        return new Bitmap(filePath);
    }

    public static async Task<Bitmap?> LoadFromWebTask(string resourcePath)
    {
        var uri = new Uri(resourcePath);
        using var httpClient = new HttpClient();
        try
        {
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AvaloniaTest", "0.1"));
            var data = await httpClient.GetByteArrayAsync(uri);
            ConsoleExt.WriteLineWithPretext("Got Image", ConsoleExt.OutputType.Info);
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{uri}' : {ex.Message}");
            return null;
        }
    }
    
    public static Bitmap LoadFromWeb(string resourcePath)
    {
        var uri = new Uri(resourcePath);
        using var httpClient = new HttpClient();
        try
        {
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AvaloniaTest", "0.1"));
            var data = httpClient.GetByteArrayAsync(uri).GetAwaiter().GetResult();
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{uri}' : {ex.Message}");
            return null;
        }
    }

    internal static SKBitmap ConvertToSkBitmap(Bitmap avaloniaBitmap)
    {
        using var memoryStream = new MemoryStream();
        avaloniaBitmap.Save(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        using var skiaStream = new SKManagedStream(memoryStream);
        var skBitmap = SKBitmap.Decode(skiaStream);
        return skBitmap;
    }

    internal static Bitmap SkBitmapToAvaloniaBitmap(SKBitmap skBitmap)
    {
        // Encode the SKBitmap to a memory stream as PNG
        using var memoryStream = new MemoryStream();
        using (var image = SKImage.FromBitmap(skBitmap))
        {
            image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(memoryStream);
        }
        memoryStream.Seek(0, SeekOrigin.Begin); // Reset the stream position to the beginning

        // Create an Avalonia bitmap from the memory stream
        var avaloniauiBitmap = new Bitmap(memoryStream);

        return avaloniauiBitmap;
    }

    internal static SKBitmap ApplyBlurEffect(SKBitmap originalImage)
    {
        SKBitmap blurredImage = new SKBitmap(originalImage.Width, originalImage.Height);
        using var canvas = new SKCanvas(blurredImage);
        canvas.Clear(SKColors.Transparent);
        canvas.DrawBitmap(originalImage, 0, 0, BlurPaint);

        return blurredImage;
    }
}