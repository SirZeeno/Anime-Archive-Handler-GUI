using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using ReactiveUI;

namespace Anime_Archive_Handler_GUI.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private SearchEngine _searchEngine;
    private int _selectedTabIndex;
    private object _selectedTabContent;
    private DataTemplate _selectedTabTemplate;
    private static DataTemplate? _generalTemplate;
    private static DataTemplate? _advancedTemplate;
    private static DataTemplate? _mediaTemplate;
    private static DataTemplate? _addonsTemplate;
    private static DataTemplate? _experimentalTemplate;

    public static DataTemplate? GeneralTemplate
    {
        get => _generalTemplate;
        set => _generalTemplate = value;
    }
    
    public static DataTemplate? AdvancedTemplate
    {
        get => _advancedTemplate;
        set => _advancedTemplate = value;
    }
    
    public static DataTemplate? MediaTemplate
    {
        get => _mediaTemplate;
        set => _mediaTemplate = value;
    }
    
    public static DataTemplate? AddonsTemplate
    {
        get => _addonsTemplate;
        set => _addonsTemplate = value;
    }
    
    public static DataTemplate? ExperimentalTemplate
    {
        get => _experimentalTemplate;
        set => _experimentalTemplate = value;
    }
    
    public SearchEngine SearchEngine
    {
        get => _searchEngine;
        set => this.RaiseAndSetIfChanged(ref _searchEngine, value);
    }
    
    public object SelectedTabContent
    {
        get => _selectedTabContent;
        set => this.RaiseAndSetIfChanged(ref _selectedTabContent, value);
    }
    
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedTabIndex, value);
    }

    public DataTemplate SelectedTabTemplate
    {
        get => _selectedTabTemplate;
        set => this.RaiseAndSetIfChanged(ref _selectedTabTemplate, value);
    }
    
    public ReactiveCommand<object, Unit> SelectionChangedCommand { get; }
    
    public SettingsViewModel()
    {
        SearchEngine = SearchEngine.Custom;
        
        SelectionChangedCommand = ReactiveCommand.Create<object>(OnSelectionChanged);
        
        // To load the General Page 
        OnSelectionChanged(new object());
    }
    
    private void OnSelectionChanged(object parameter)
    {
        switch (SelectedTabIndex)
        {
            case 0:
            {
                if (GeneralTemplate != null)
                {
                    SelectedTabContent = GeneralTemplate;
                    SelectedTabTemplate = GeneralTemplate;
                }
                else
                {
                    ConsoleExt.WriteLineWithPretext("Advanced Template not found", ConsoleExt.OutputType.Error);
                }

                break;
            }
            case 1:
            {
                if (AdvancedTemplate != null)
                {
                    SelectedTabContent = AdvancedTemplate;
                    SelectedTabTemplate = AdvancedTemplate;
                }
                else
                {
                    ConsoleExt.WriteLineWithPretext("Advanced Template not found", ConsoleExt.OutputType.Error);
                }

                break;
            }
            case 2:
            {
                if (MediaTemplate != null)
                {
                    SelectedTabContent = MediaTemplate;
                    SelectedTabTemplate = MediaTemplate;
                }
                else
                {
                    ConsoleExt.WriteLineWithPretext("Media Template not found", ConsoleExt.OutputType.Error);
                }

                break;
            }
            case 3:
            {
                if (AddonsTemplate != null)
                {
                    SelectedTabContent = AddonsTemplate;
                    SelectedTabTemplate = AddonsTemplate;
                }
                else
                {
                    ConsoleExt.WriteLineWithPretext("Addons Template not found", ConsoleExt.OutputType.Error);
                }

                break;
            }
            case 4:
            {
                if (ExperimentalTemplate != null)
                {
                    SelectedTabContent = ExperimentalTemplate;
                    SelectedTabTemplate = ExperimentalTemplate;
                }
                else
                {
                    ConsoleExt.WriteLineWithPretext("Experimental Template not found", ConsoleExt.OutputType.Error);
                }

                break;
            }
        }
    }
}