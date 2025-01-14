using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Windowing;

namespace Anime_Archive_Handler_GUI.Views;

public partial class PopupView : Window
{
    public PopupView()
    {
        InitializeComponent();
        
    }
    
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}