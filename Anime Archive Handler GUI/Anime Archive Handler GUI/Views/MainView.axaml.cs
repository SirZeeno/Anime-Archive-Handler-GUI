using System;
using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;

namespace Anime_Archive_Handler_GUI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        LoadImageAsync("https://cdn.myanimelist.net/images/anime/4/19644l.jpg");
    }
    
    private async void LoadImageAsync(string imageUrl)
    {
        try
        {
            using var client = new HttpClient();
            // Download the image data
            var response = await client.GetAsync(imageUrl);
            response.EnsureSuccessStatusCode();

            // Read the image data into a stream
            await using var stream = await response.Content.ReadAsStreamAsync();
            // Load the stream into a Bitmap
            var bitmap = new Bitmap(stream);

            // Set the Source of the Image control
            this.FindControl<Image>("OnlineImage1")!.Source = bitmap;
            this.FindControl<Image>("OnlineImage2")!.Source = bitmap;
            this.FindControl<Image>("OnlineImage3")!.Source = bitmap;
            this.FindControl<Image>("OnlineImage4")!.Source = bitmap;
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine("Exception occurred while loading image: " + ex.Message);
        }
    }
    
    public void AddColumnToGrid()
    {
        var columnDefinition = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
        DynamicGrid.ColumnDefinitions.Add(columnDefinition);

        // You can now add controls to the new column
        var newControl = new TextBlock { Text = "New Column Content" };
        Grid.SetColumn(newControl, DynamicGrid.ColumnDefinitions.Count - 1);
        DynamicGrid.Children.Add(newControl);
    }
}