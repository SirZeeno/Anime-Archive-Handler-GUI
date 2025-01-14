using Avalonia;
using Avalonia.Markup.Xaml;

namespace Anime_Archive_Handler_GUI.UnitTesting;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}