using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{
    public Transform player;

    public PerlinNoise instance;

    public Slider HPbar;
    public Slider HungerBar;

    public TMP_Text groundSpeedVisible;
    public TMP_Text seaSpeedVisible;
    public TMP_Text mountainSpeedVisible;

    public TMP_Text scoreValue;
    public GameObject escMenu;

    public static int playerMaximumGroundSpeed = 0;
    public static int playerMaximumSeaSpeed = 0;
    public static int playerMaximumMountainSpeed = 0;

    public static int playerGroundSpeed;
    public static int playerSeaSpeed;
    public static int playerMountainSpeed;

    public static bool isPlayerTurn = true;
    public static bool isPlayerAlive = true;

    public static List<DamageRoll> playerDice = new List<DamageRoll>();

    public static float playerCurrentHP = 20;
    public static int playerMaxHP = 20;
    public static float playerCurrentHunger = 50;
    public static int playerMaxHunger = 50;

    // Player stats
    public static List<Limb> currentLimbs = new List<Limb>();
    
    //private int playerExp = 0;
    //private int playerLvl = 0;
    private const int tileMovementHungerWaste = 1;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => instance.generationComplete);

        double xRandom = Math.Round((float)Random.Range(0, WorldData.worldWidth - 1));
        double yRandom = Math.Round((float)Random.Range(0, WorldData.worldHeight - 1));

        transform.position = new Vector3((float)xRandom, (float)yRandom, 0f);

        GameObject centralTile = GameObject.Find(string.Format("tile_x{0}_y{1}", xRandom, yRandom));

        if (centralTile.transform.parent.name == "GroundTile")
        {
            Debug.Log("You're a ground boy!");
            currentLimbs.Add(ExistingLimbs.Arm);
            currentLimbs.Add(ExistingLimbs.Leg);
            currentLimbs.Add(ExistingLimbs.Leg);
        }
        else if (centralTile.transform.parent.name == "SeaTile")
        {
            Debug.Log("You're a water girl!");
            currentLimbs.Add(ExistingLimbs.Jaw);
            currentLimbs.Add(ExistingLimbs.Fin);
            currentLimbs.Add(ExistingLimbs.Fin);
        }
        else if (centralTile.transform.parent.name == "MountainTile")
        {
            Debug.Log("You're a flying... Err... Someone..!");
            currentLimbs.Add(ExistingLimbs.Claws);
            currentLimbs.Add(ExistingLimbs.Wings);
        }
        else
        {
            Debug.Log("Unknown tile type");
        }

        bool hasFirstArm = false;
        for (int i = 0; i < currentLimbs.Count; i++)
        {
            if (currentLimbs[i] == ExistingLimbs.Leg || (currentLimbs[i] == ExistingLimbs.Arm && hasFirstArm))
            {
                playerMaximumGroundSpeed++;
            }
            else if (currentLimbs[i] == ExistingLimbs.Arm && !hasFirstArm)
            {
                hasFirstArm = true;
                playerDice.Add(new DamageRoll(1, 8, 0));

            }
            else if (currentLimbs[i] == ExistingLimbs.Fin)
            {
                playerMaximumSeaSpeed++;
            }    
            else if (currentLimbs[i] == ExistingLimbs.Wings)
            {
                playerMaximumGroundSpeed++;
                playerMaximumSeaSpeed++;
                playerMaximumMountainSpeed++;
            }
            else if (currentLimbs[i] == ExistingLimbs.Jaw)
            {
                playerDice.Add(new DamageRoll(2, 8, 2));
            }
            else if (currentLimbs[i] == ExistingLimbs.Claws)
            {
                playerDice.Add(new DamageRoll(1, 6, 0));
            }

            playerGroundSpeed = playerMaximumGroundSpeed;
            playerSeaSpeed = playerMaximumSeaSpeed;
            playerMountainSpeed = playerMaximumMountainSpeed;
        }

        HungerBar.maxValue = playerMaxHunger;
        HPbar.maxValue = playerMaxHP;
    }

    private void Update()
    {
        if (playerCurrentHunger < 0)
        {
            playerCurrentHP += playerCurrentHunger;
            playerCurrentHunger = 0;
        }

        if (playerCurrentHP <= 0)
        {
            isPlayerAlive = false;
            Time.timeScale = 0;
            escMenu.SetActive(true);
            scoreValue.text = Convert.ToString(BattleSystem.score);
        }
        HungerBar.value = playerCurrentHunger;
        HPbar.value = playerCurrentHP;

        groundSpeedVisible.text = Convert.ToString(playerGroundSpeed);
        seaSpeedVisible.text = Convert.ToString(playerSeaSpeed);
        mountainSpeedVisible.text = Convert.ToString(playerMountainSpeed);

    }
}
