using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject credits;
    public GameObject mainMenu;

    public void GoBack()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }
}