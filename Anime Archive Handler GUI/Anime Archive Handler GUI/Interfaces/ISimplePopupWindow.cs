using System.ComponentModel;

namespace Anime_Archive_Handler_GUI.Interfaces;

public interface ISimplePopupWindow
{
    public void ShowPopup();
    public void ExitPopup();
    public void PopupInteraction();
}