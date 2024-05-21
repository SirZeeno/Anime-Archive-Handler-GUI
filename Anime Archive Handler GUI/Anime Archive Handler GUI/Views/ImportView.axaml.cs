using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia.Controls;

namespace Anime_Archive_Handler_GUI.Views;

public partial class ImportView : Window
{
    public ImportView()
    {
        InitializeComponent();
        
        ImportHandler.ImportWindowInstance = this;
    }
}