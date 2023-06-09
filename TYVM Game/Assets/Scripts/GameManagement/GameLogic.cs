using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

    [SerializeField]
    private int totalEnemies;
    private int enemiesRemaining;
    private int stageNumber;
    private GameObject player;
    private GameObject[] enemies;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        enemiesRemaining = totalEnemies;
        Debug.Log("Total Enemies: " + totalEnemies);
        stageNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update() {

    }

    public void EnemyDefeated(GameObject enemy) {
        enemiesRemaining--;
        if (enemiesRemaining <= 0) {
            TriggerVictory();
        }
    }

    private void TriggerVictory() {
        // TODO: add stageNumber to a list of cleared stages. 
        // Feel free to add achievements, coins etc. to this method later on.
        Debug.Log("Stage " + stageNumber + " cleared!");
        // Disable player controls (commented out for now to allow us to continue testing other things after killing enemies)
        // player.GetComponent<PlayerMovement>().enabled = false;
        // player.GetComponent<Shooting>().enabled = false;
        StageSummary();
    }

    public void TriggerDefeat() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Disable all remaining enemies
        foreach (GameObject e in enemies) {
            e.GetComponent<EnemyMovement>().enabled = false;
            e.GetComponent<EnemyShooting>().enabled = false;
        }
        Debug.Log("You died!");
        StageSummary();
        // TODO: transit to you lose screen
    }

    private void StageSummary() {
        Debug.Log("#####Summary#####");
        Debug.Log("Enemies defeated: " + (totalEnemies - enemiesRemaining) + " of " + totalEnemies);
        Debug.Log("Time Elapsed (seconds): " + System.Math.Round(Time.time, 2));
    }
}
