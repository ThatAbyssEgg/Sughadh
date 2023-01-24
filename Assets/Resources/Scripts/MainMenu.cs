using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject credits;
    public GameObject worldSettings;
    public void PlayGame()
    {
        worldSettings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void LoadSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void LoadCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
}