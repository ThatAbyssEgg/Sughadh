using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static int score = 0;

    public void EndTurnClicked()
    {
        PlayerScript.playerCurrentHunger -= 3;
        score += 10;

        SwitchTurn();
        for (int i = 0; i < PerlinNoise.generatedEnemies.Count; i++)
        {
            if (PerlinNoise.generatedEnemies[i].transform.parent.name == "Nests")
            {
                NestAI.EnemyGeneration(PerlinNoise.generatedEnemies[i]);
            }
            else if (PerlinNoise.generatedEnemies[i].transform.parent.name.Contains("nest_"))
            {
                NeutralAI.NeutralBehavior(PerlinNoise.generatedEnemies[i]);
            }
        }

        PlayerScript.playerGroundSpeed = PlayerScript.playerMaximumGroundSpeed;
        PlayerScript.playerSeaSpeed = PlayerScript.playerMaximumSeaSpeed;
        PlayerScript.playerMountainSpeed = PlayerScript.playerMaximumMountainSpeed;

        SwitchTurn();
    }
    private void SwitchTurn()
    {
        PlayerScript.isPlayerTurn = !PlayerScript.isPlayerTurn;
    }
}
