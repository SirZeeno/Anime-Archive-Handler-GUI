using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Anime_Archive_Handler_GUI.Views;

public partial class ImportWindow : Window
{
    public ImportWindow()
    {
        InitializeComponent();
    }
    
    public void AddPathToQueue(string path)
    {
        if (Path.Exists(path))
        {
            
        }
    }
}