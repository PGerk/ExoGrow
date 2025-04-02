using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    private CanvasGroup mainMenu, optionsMenu;

    void Awake()
    {
        mainMenu = GameObject.Find("Main Menu").GetComponent<CanvasGroup>();
        optionsMenu = GameObject.Find("Options Menu").GetComponent<CanvasGroup>();

        HideMenu(optionsMenu);
        ShowMenu(mainMenu);
    }

    public void SwitchToOptionsMenu()
    {
        HideMenu(mainMenu);
        ShowMenu(optionsMenu);
    }

    public void SwitchToMainMenu()
    {
        HideMenu(optionsMenu);
        ShowMenu(mainMenu);
    }

    void ShowMenu(CanvasGroup menu)
    {
        menu.alpha = 1f;
        menu.interactable = true;
        menu.blocksRaycasts = true;
    }

    void HideMenu(CanvasGroup menu)
    {
        menu.alpha = 0f;
        menu.interactable = false;
        menu.blocksRaycasts = false;
    }
}
