using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using Anime_Archive_Handler_GUI;

[assembly: SupportedOSPlatform("browser")]

internal sealed partial class Program
{
    private static Task Main(string[] args) => BuildAvaloniaApp()
        .WithInterFont()
        .UseReactiveUI()
        .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}