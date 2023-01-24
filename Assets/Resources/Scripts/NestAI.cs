using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NestAI : MonoBehaviour
{
    
    
    private const int standardEnemyMaxAmount = 5;
    private int enemyLimit = (int)Math.Round(standardEnemyMaxAmount * WorldData.worldEnemyRate);
    private int enemiesSpawned = 0;

    public static void EnemyGeneration(GameObject currentNest)
    {
        var nestAI = new NestAI();

        if (!PlayerScript.isPlayerTurn && nestAI.enemiesSpawned < nestAI.enemyLimit && currentNest.transform.childCount < standardEnemyMaxAmount * WorldData.worldEnemyRate)
        {
            nestAI.enemiesSpawned++;
            GameObject newbornEnemy;
            int enemyBehavior = Random.Range(0, 2);
            string behaviorPrefix, enemyPostfix;

            // Commented this out because I might not have enough time to create each type of the AI. I'll stop on neutral for now

            //switch (enemyBehavior)
            //{
            //    case 0: behaviorPrefix = "Coward"; break;
            //    case 1: behaviorPrefix = "Neutral"; break;
            //    case 2: behaviorPrefix = "Hostile"; break;
            //    default: behaviorPrefix = "ERR: Behavior not found"; break;
            //}

            behaviorPrefix = "Neutral";

            if (currentNest.transform.name.Contains("ground"))
            {
                enemyPostfix = "LandFoe";
            }
            else if (currentNest.transform.name.Contains("sea"))
            {
                enemyPostfix = "SeaFoe";
            }
            else if (currentNest.transform.name.Contains("mountain"))
            {
                enemyPostfix = "FlyingFoe";
            }
            else
            {
                enemyPostfix = "ERR: Foe not found";
            }

            newbornEnemy = Instantiate(nestAI.LoadEnemy(behaviorPrefix, enemyPostfix));
            PerlinNoise.generatedEnemies.Add(newbornEnemy);
            newbornEnemy.transform.SetParent(currentNest.transform);
            newbornEnemy.transform.localPosition = new Vector3(0, 0, 0);

            SpriteRenderer enemyRenderer = newbornEnemy.GetComponent<SpriteRenderer>();
            enemyRenderer.sortingLayerName = "GeneratedSprites";
        }
    }

    private GameObject LoadEnemy (string prefabPrefix, string prefabPostfix)
    {
        var loadedObject = Resources.Load(string.Format("Prefabs/{0}/{1}{2}", prefabPrefix, prefabPrefix.ToLower(), prefabPostfix));

        if (loadedObject == null)
        {
            Debug.Log(string.Format("Prefabs/{0}/{1}{2}", prefabPrefix, prefabPrefix.ToLower(), prefabPostfix));
            throw new FileNotFoundException("No foe found with given name");
        }

        return (GameObject)loadedObject;
    }
}
