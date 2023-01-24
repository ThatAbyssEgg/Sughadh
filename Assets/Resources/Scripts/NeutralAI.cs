using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class NeutralAI : MonoBehaviour
{
    DamageRoll foeAttack = new DamageRoll(1, 8, 0);
    int foeMaxHP = 10 + (int)Math.Round((double)BattleSystem.score / 1000);
    int foeCurrentHP = 10 + (int)Math.Round((double)BattleSystem.score / 1000);
    bool isAlive = true;


    public static void NeutralBehavior(GameObject neutralEnemy)
    {
        NeutralAI ai = new NeutralAI();

        if (ai.foeCurrentHP == ai.foeMaxHP)
        {
            //if (neutralEnemy.transform.parent.name.Contains("ground"))
            //{

            //}
            //else if (neutralEnemy.transform.parent.name.Contains("sea"))
            //{

            //}
            //else if (neutralEnemy.transform.parent.name.Contains("mountain"))
            //{

            //}
            //else
            //{
            //    Debug.Log("Parent not found");
            //}
            List<GameObject> nearbyTiles = new List<GameObject>();

            GameObject sideTile;
            for (int i = 0; i < 4; i++)
            {
                int direction = (int)Math.Pow(-1, i);

                if (i <= 1)
                {
                    sideTile = GameObject.Find(string.Format("tile_x{0}_y{1}", neutralEnemy.transform.localPosition.x + 1 * direction, neutralEnemy.transform.localPosition.y));
                }
                else
                {
                    sideTile = GameObject.Find(string.Format("tile_x{0}_y{1}", neutralEnemy.transform.localPosition.x, neutralEnemy.transform.localPosition.y + 1 * direction));
                }
                if (sideTile != null)
                {
                    nearbyTiles.Add(sideTile);
                }
            }

            int randomDirection = Random.Range(0, nearbyTiles.Count);
            neutralEnemy.transform.localPosition = nearbyTiles[randomDirection].transform.localPosition;
        }

        else
        {
            GameObject player = GameObject.Find("Player");
            if (Vector3.Distance(neutralEnemy.transform.localPosition, player.transform.localPosition) <= 1.44)
            {
                for (int i = 0; i < ai.foeAttack.diceAmount; i++)
                {
                    PlayerScript.playerCurrentHP -= Random.Range(1, ai.foeAttack.diceMaxValue) + ai.foeAttack.diceModifier;
                }    
            }
            else
            {
                int xDistance = (int)(player.transform.localPosition.x - neutralEnemy.transform.localPosition.x);
                int yDistance = (int)(player.transform.localPosition.y - neutralEnemy.transform.localPosition.y);

                // Hardcoding the pathfinding cause it's 2 hours left lol
                
                // Up
                if (yDistance > 0 && yDistance >= Math.Abs(xDistance))
                {
                    neutralEnemy.transform.localPosition = new Vector3(neutralEnemy.transform.localPosition.x, neutralEnemy.transform.localPosition.y + 1, 0);
                }

                // Down
                else if (yDistance < 0 && Math.Abs(yDistance) >= Math.Abs(xDistance))
                {
                    neutralEnemy.transform.localPosition = new Vector3(neutralEnemy.transform.localPosition.x, neutralEnemy.transform.localPosition.y - 1, 0);
                }

                // Right
                else if (xDistance > 0 && xDistance >= Math.Abs(yDistance))
                {
                    neutralEnemy.transform.localPosition = new Vector3(neutralEnemy.transform.localPosition.x + 1, neutralEnemy.transform.localPosition.y, 0);
                }

                // Left
                else if (xDistance < 0 && Math.Abs(xDistance) >= Math.Abs(yDistance))
                {
                    neutralEnemy.transform.localPosition = new Vector3(neutralEnemy.transform.localPosition.x - 1, neutralEnemy.transform.localPosition.y, 0);
                }
                

            }
        }
    }

    private void Update()
    {
        if (foeCurrentHP <= 0)
        {
            isAlive = false;
            BattleSystem.score += 100;
            PlayerScript.playerCurrentHunger += 10;
            if (PlayerScript.playerCurrentHunger < PlayerScript.playerMaxHunger)
            {
                PlayerScript.playerCurrentHunger = PlayerScript.playerMaxHunger;
            }

            int lootChance = Random.Range(0, 2);
            if (lootChance == 2)
            {
                PlayerScript.currentLimbs.Add(limbGen.GetRandomLimb());
            }
            gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && PlayerScript.isPlayerTurn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction, 1);
            if (raycastHit.collider != null)
            {
                Debug.Log(string.Format("You're attacking the foe {0}!", raycastHit.transform.gameObject.name));
                for (int i = 0; i < PlayerScript.playerDice.Count; i++)
                {
                    for (int j = 0; j < PlayerScript.playerDice[i].diceAmount; j++)
                    {
                        foeCurrentHP -= Random.Range(1, PlayerScript.playerDice[i].diceMaxValue) + PlayerScript.playerDice[i].diceModifier;
                    }
                    Debug.Log(string.Format("Dealt {0} damage!", PlayerScript.playerDice[i].diceAmount * PlayerScript.playerDice[i].diceMaxValue) + PlayerScript.playerDice[i].diceModifier);
                }

            }
        }
    }
}
