using Anime_Archive_Handler_GUI.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using FluentAvalonia.UI.Windowing;

namespace Anime_Archive_Handler_GUI.Views;

public partial class SettingsView : AppWindow
{
    public SettingsView()
    {
        InitializeComponent();
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        
        RetrieveResource();
    }
    
    private void RetrieveResource()
    {
        if (this.TryFindResource("General", out var general))
        {
            // Use the retrieved resource
            var dataTemplate = general as DataTemplate;
            // Apply the data template
           SettingsViewModel.GeneralTemplate = dataTemplate;
        }
        else
        {
            // Handle the case where the resource is not found
            ConsoleExt.WriteLineWithPretext("General not found", ConsoleExt.OutputType.Error);
        }
        if (this.TryFindResource("Advanced", out var advanced))
        {
            // Use the retrieved resource
            var dataTemplate = advanced as DataTemplate;
            // Apply the data template
            SettingsViewModel.AdvancedTemplate = dataTemplate;
        }
        else
        {
            // Handle the case where the resource is not found
            ConsoleExt.WriteLineWithPretext("Advanced not found", ConsoleExt.OutputType.Error);
        }
        if (this.TryFindResource("Media", out var media))
        {
            // Use the retrieved resource
            var dataTemplate = media as DataTemplate;
            // Apply the data template
            SettingsViewModel.MediaTemplate = dataTemplate;
        }
        else
        {
            // Handle the case where the resource is not found
            ConsoleExt.WriteLineWithPretext("Media not found", ConsoleExt.OutputType.Error);
        }
        if (this.TryFindResource("Addons", out var addons))
        {
            // Use the retrieved resource
            var dataTemplate = addons as DataTemplate;
            // Apply the data template
            SettingsViewModel.AddonsTemplate = dataTemplate;
        }
        else
        {
            // Handle the case where the resource is not found
            ConsoleExt.WriteLineWithPretext("Addons not found", ConsoleExt.OutputType.Error);
        }
        if (this.TryFindResource("Experimental", out var experimental))
        {
            // Use the retrieved resource
            var dataTemplate = experimental as DataTemplate;
            // Apply the data template
            SettingsViewModel.ExperimentalTemplate = dataTemplate;
        }
        else
        {
            // Handle the case where the resource is not found
            ConsoleExt.WriteLineWithPretext("Experimental not found", ConsoleExt.OutputType.Error);
        }
    }
}