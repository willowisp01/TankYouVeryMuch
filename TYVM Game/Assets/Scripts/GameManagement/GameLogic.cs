using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    [SerializeField]
    private ScreenPrinter screenPrinter;

    private int totalEnemies;
    private int enemiesRemaining;
    private GameObject player;
    private GameObject[] enemies;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        enemiesRemaining = totalEnemies;
    }

    public void TankDefeated(GameObject tank) {
        if (tank.CompareTag("Player")) {
            TriggerDefeat();
        } else {
            enemiesRemaining--;
            if (enemiesRemaining <= 0) {
                TriggerVictory();
            }
        }
    }

    // Disable player controls after victory
    private void DisablePlayer() {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Shooting>().enabled = false;
    }

    // Disable all remaining enemies after defeat
    private void DisableEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies) {
            e.GetComponent<EnemyMovement>().enabled = false;
            e.GetComponent<EnemyShooting>().enabled = false;
            e.GetComponentInChildren<AIPath>().enabled = false;
        }
    }

    private void TriggerVictory() {
        // TODO: add stageNumber to a list of cleared stages. 
        // Feel free to add achievements, coins etc. to this method later on.
        // DisablePlayer(); Commented out for now to allow us to continue testing other things after killing enemies
        screenPrinter.Result("VICTORY");
    }

    private void TriggerDefeat() {
        DisableEnemies();
        //Debug.Log("You died!");
        screenPrinter.Result("DEFEAT");
    }

    public string StageSummary() {
        return "Enemies defeated: " + (totalEnemies - enemiesRemaining) + " of " + totalEnemies + "\n" +
            "Time elapsed (seconds): " + System.Math.Round(Time.timeSinceLevelLoad, 2);
    }
}
