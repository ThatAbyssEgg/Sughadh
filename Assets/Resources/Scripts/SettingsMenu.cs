using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;

    public AudioMixer audioMixer;

    public void GoBack()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetGraphicQuality(int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex); 
    }
}