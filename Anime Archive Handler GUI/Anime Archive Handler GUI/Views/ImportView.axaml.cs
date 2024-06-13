using Anime_Archive_Handler_GUI.ViewModels;
using FluentAvalonia.UI.Windowing;

namespace Anime_Archive_Handler_GUI.Views;

public partial class ImportView : AppWindow
{
    public ImportView()
    {
        InitializeComponent();
        
        ImportHandler.ImportWindowInstance = this;
        ImportViewModel.AnimeItemsControlInstance = AnimeItemsControl;
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }
}