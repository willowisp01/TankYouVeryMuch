using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    [SerializeField]
    private ScreenPrinter screenPrinter;

    [SerializeField]
    private Button nextStageButton;

    private int totalEnemies;
    private int enemiesRemaining;
    private GameObject player;
    private GameObject[] enemies;
    private StageManager stageManager;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        enemiesRemaining = totalEnemies;
        stageManager = GetComponent<StageManager>();
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
    public void DisablePlayer() {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Shooting>().enabled = false;
    }

    public void EnablePlayer() {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Shooting>().enabled = true;
    }

    // Disable all remaining enemies after defeat
    public void DisableEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies) {
            e.GetComponent<EnemyMovement>().enabled = false;
            e.GetComponent<EnemyShooting>().enabled = false;
            e.GetComponentInChildren<AIPath>().enabled = false;
        }
    }

    public void EnableEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies) {
            e.GetComponent<EnemyMovement>().enabled = true;
            e.GetComponent<EnemyShooting>().enabled = true;
            e.GetComponentInChildren<AIPath>().enabled = true;
        }
    }

    private void TriggerVictory() {
        // TODO: add stageNumber to a list of cleared stages. 
        // Feel free to add achievements, coins etc. to this method later on.
        DisablePlayer();
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1) {
            nextStageButton.interactable = true;
            nextStageButton.onClick.AddListener(stageManager.Next);
        }
        screenPrinter.Result("VICTORY");
    }

    private void TriggerDefeat() {
        DisableEnemies();
        screenPrinter.Result("DEFEAT");
    }

    public string StageSummary() {
        return "Enemies defeated: " + (totalEnemies - enemiesRemaining) + " of " + totalEnemies + "\n" +
            "Time elapsed (seconds): " + System.Math.Round(Time.timeSinceLevelLoad, 2);
    }
}
