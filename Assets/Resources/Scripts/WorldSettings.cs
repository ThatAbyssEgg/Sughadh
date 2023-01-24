using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   

public class WorldSettings : MonoBehaviour
{
    public GameObject worldSettings;
    public GameObject mainMenu;
    public TMP_Dropdown worldSizeOptions;
    public TMP_Dropdown enemyAmountOptions;

    private const int baseWorldSize = 16;

    private List<float> enemySpawnRates = new List<float>() { (float)0.25, (float)0.5, 1, 2, 5, 10 };

    private void Start()
    {
        WorldData.worldEnemyRate = (float)0.25;
        WorldData.worldHeight = 16;
        WorldData.worldWidth = 16;
    }
    public void GoBack()
    {
        worldSettings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void BeBorn()
    {
        SceneManager.LoadScene("Resources/Scenes/GameItself");
    }

    public void SetWorldSize()
    {
        WorldData.worldWidth = baseWorldSize * (int)Math.Pow(2, (worldSizeOptions.value));
        WorldData.worldHeight = baseWorldSize * (int)Math.Pow(2, (worldSizeOptions.value));
    }    

    public void SetEnemyAmountModifier()
    {
        WorldData.worldEnemyRate = enemySpawnRates[enemyAmountOptions.value];
    }    
}
