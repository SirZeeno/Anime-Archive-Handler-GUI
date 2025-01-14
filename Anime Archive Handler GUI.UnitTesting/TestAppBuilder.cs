using Anime_Archive_Handler_GUI.UnitTesting;
using Avalonia;
using Avalonia.Headless;


[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]
namespace Anime_Archive_Handler_GUI.UnitTesting;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}