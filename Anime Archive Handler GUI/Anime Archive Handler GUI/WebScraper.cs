using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Anime_Archive_Handler_GUI;

using static Regex;
using static FileHandler;

public static partial class WebScraper
{
    private static readonly HttpClient HttpClient = new();

    private static async Task DownloadPicture(string[] links)
    {
        const string pattern = "(?i)-Latest\\."; //need to change this so it can determine if it has .jpg, .png, .webm, or any other picture format in the url
        const string pattern2 = "(?i)-Latest\\."; //need to change this so to remove everything but the file name plus extension
        foreach (var link in links)
        {
            try
            {
                var match = Match(link, pattern);
                switch (match.Success)
                {
                    case true:
                    {
                        var response = await HttpClient.GetAsync(link);
                    
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as a byte array
                            var fileBytes = await response.Content.ReadAsByteArrayAsync();

                            // Specify the local file path where you want to save the downloaded file
                            var localFilePath = Path.Combine(GetDirectoryInProgramFolder("Downloads"), "the string before the file extension format"); //<---------
                            
                            // Write the byte array to the local file
                            await File.WriteAllBytesAsync(localFilePath, fileBytes);

                            ConsoleExt.WriteLineWithPretext("File downloaded successfully.", ConsoleExt.OutputType.Info);
                        }
                    
                        else
                        {
                            string message = $"Failed to download file. Status code: {response.StatusCode}";
                            ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new Exception("message"));
                        }

                        break;
                    }
                    case false:
                    {
                        // Send an HTTP GET request and retrieve the HTML content asynchronously.
                        var htmlContent = await HttpClient.GetStringAsync(link);
                
                        // Parse and process the HTML content using HtmlAgilityPack.
                        var htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(htmlContent);
                
                        // Extracting all the links on the page
                        var nodeCollection = htmlDoc.DocumentNode.SelectNodes("//a[@src]");
                        if (nodeCollection != null)
                        {
                            foreach (var node in nodeCollection)
                            {
                                // Extract the href attribute value of each link
                                var src = node.GetAttributeValue("href", "");

                                var match2 = Match(src, pattern2);
                                if (!match2.Success) continue;
                    
                                ConsoleExt.WriteLineWithPretext(src, ConsoleExt.OutputType.Info);
                    
                                // Ensure that the URL is correctly formed
                                var fullUrl = new Uri(new Uri(link), src).ToString();

                                var response = await HttpClient.GetAsync(fullUrl);
                    
                                if (response.IsSuccessStatusCode)
                                {
                                    // Read the response content as a byte array
                                    var fileBytes = await response.Content.ReadAsByteArrayAsync();

                                    // Specify the local file path where you want to save the downloaded file
                                    var localFilePath = Path.Combine(GetDirectoryInProgramFolder("Downloads"), "the string before the file extension format");
                            
                                    // Write the byte array to the local file
                                    await File.WriteAllBytesAsync(localFilePath, fileBytes);

                                    ConsoleExt.WriteLineWithPretext("File downloaded successfully.", ConsoleExt.OutputType.Info);
                                }
                    
                                else
                                {
                                    string message = $"Failed to download file. Status code: {response.StatusCode}";
                                    ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new Exception("message"));
                                }
                            }
                        }

                        break;
                    }
                }
            }
            catch (Exception? e)
            {
                ConsoleExt.WriteLineWithPretext(e, ConsoleExt.OutputType.Error, e);
            
                throw;
            }
        }
    }
    
    private static async Task DownloadVideos(string[] links)
    {
        const string pattern = "(?i)-Latest\\."; //need to change this so it can determine if it has .mp4 in the url
        const string pattern2 = "(?i)-Latest\\."; //need to change this so to remove everything but the file name plus extension
        foreach (var link in links)
        {
            try
            {
                var match = Match(link, pattern);
                switch (match.Success)
                {
                    case true:
                    {
                        var response = await HttpClient.GetAsync(link);
                    
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as a byte array
                            var fileBytes = await response.Content.ReadAsByteArrayAsync();

                            // Specify the local file path where you want to save the downloaded file
                            var localFilePath = Path.Combine(GetDirectoryInProgramFolder("Downloads"), "the string before the .mp4"); //<---------
                            
                            // Write the byte array to the local file
                            await File.WriteAllBytesAsync(localFilePath, fileBytes);

                            ConsoleExt.WriteLineWithPretext("File downloaded successfully.", ConsoleExt.OutputType.Info);
                        }
                    
                        else
                        {
                            string message = $"Failed to download file. Status code: {response.StatusCode}";
                            ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new Exception("message"));
                        }

                        break;
                    }
                    case false:
                    {
                        // Send an HTTP GET request and retrieve the HTML content asynchronously.
                        var htmlContent = await HttpClient.GetStringAsync(link);
                
                        // Parse and process the HTML content using HtmlAgilityPack.
                        var htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(htmlContent);
                
                        // Extracting all the links on the page
                        var nodeCollection = htmlDoc.DocumentNode.SelectNodes("//a[@src]");
                        if (nodeCollection != null)
                        {
                            foreach (var node in nodeCollection)
                            {
                                // Extract the href attribute value of each link
                                var src = node.GetAttributeValue("href", "");

                                var match2 = Match(src, pattern2);
                                if (!match2.Success) continue;
                    
                                ConsoleExt.WriteLineWithPretext(src, ConsoleExt.OutputType.Info);
                    
                                // Ensure that the URL is correctly formed
                                var fullUrl = new Uri(new Uri(link), src).ToString();

                                var response = await HttpClient.GetAsync(fullUrl);
                    
                                if (response.IsSuccessStatusCode)
                                {
                                    // Read the response content as a byte array
                                    var fileBytes = await response.Content.ReadAsByteArrayAsync();

                                    // Specify the local file path where you want to save the downloaded file
                                    var localFilePath = Path.Combine(GetDirectoryInProgramFolder("Downloads"), "the string before the .mp4");
                            
                                    // Write the byte array to the local file
                                    await File.WriteAllBytesAsync(localFilePath, fileBytes);

                                    ConsoleExt.WriteLineWithPretext("File downloaded successfully.", ConsoleExt.OutputType.Info);
                                }
                    
                                else
                                {
                                    string message = $"Failed to download file. Status code: {response.StatusCode}";
                                    ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new Exception("message"));
                                }
                            }
                        }

                        break;
                    }
                }
            }
            catch (Exception? e)
            {
                ConsoleExt.WriteLineWithPretext(e, ConsoleExt.OutputType.Error, e);
            
                throw;
            }
        }
    }
    
    public static async Task ScrapeAnimetoshoExports()
    {
        const string url = "https://storage.animetosho.org/dbexport/";

        try
        {
            // Send an HTTP GET request and retrieve the HTML content asynchronously.
            var htmlContent = await HttpClient.GetStringAsync(url);
            
            // Parse and process the HTML content using HtmlAgilityPack.
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            
            // Extracting all the links on the page
            var links = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            if (links != null)
            {
                foreach (var link in links)
                {
                    // Extract the href attribute value of each link
                    var href = link.GetAttributeValue("href", "");

                    var match = MyRegex().Match(href);
                    if (!match.Success) continue;
                    
                    ConsoleExt.WriteLineWithPretext(href, ConsoleExt.OutputType.Info);
                    
                    // Ensure that the URL is correctly formed
                    var fullUrl = new Uri(new Uri(url), href).ToString();

                    var response = await HttpClient.GetAsync(fullUrl);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a byte array
                        var fileBytes = await response.Content.ReadAsByteArrayAsync();

                        // Specify the local file path where you want to save the downloaded file
                        var localFilePath = Path.Combine(GetDirectoryInProgramFolder("Downloads"), href);
                            
                        // Write the byte array to the local file
                        await File.WriteAllBytesAsync(localFilePath, fileBytes);

                        ConsoleExt.WriteLineWithPretext("File downloaded successfully.", ConsoleExt.OutputType.Info);
                    }
                    
                    else
                    {
                        string message = $"Failed to download file. Status code: {response.StatusCode}";
                        ConsoleExt.WriteLineWithPretext(message, ConsoleExt.OutputType.Error, new Exception("message"));
                    }
                }
            }
        }
        catch (Exception? e)
        {
            ConsoleExt.WriteLineWithPretext(e, ConsoleExt.OutputType.Error, e);
            
            throw;
        }
    }

    [GeneratedRegex(@"(?i)-Latest\.", RegexOptions.None, "en-US")]
    private static partial Regex MyRegex();
}