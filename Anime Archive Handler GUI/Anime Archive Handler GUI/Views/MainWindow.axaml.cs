using Anime_Archive_Handler_GUI.ViewModels;
using FluentAvalonia.UI.Windowing;

namespace Anime_Archive_Handler_GUI.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }
}