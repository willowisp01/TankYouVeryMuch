using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private float time = 0f;

    private bool stopwatchRunning = false;
    private int totalEnemies = 0;
    private int enemiesRemaining = 0;
    public int stageNumber;
    public GameObject[] enemies;


    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        enemiesRemaining = totalEnemies;
        Debug.Log("Total Enemies: " + totalEnemies);

        restartStopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        updateStopwatch();
    }

    public void enemyDefeated() {
        enemiesRemaining--;
        if (allEnemiesDefeated()){
            triggerVictory(stageNumber);
        }
    }

    private bool allEnemiesDefeated() { //check if there are any remaining enemies.
    Debug.Log("Enemies Remaining: " + enemiesRemaining);
        if (enemiesRemaining <= 0) {
            return true;
        }
        return false;
    }

    private void triggerVictory(int stageNumber) {
        //TODO: add stageNumber to a list of cleared stages. 
        //feel free to add achievements, coins etc. to this method later on.
        stopStopwatch();
        Debug.Log("Stage " + stageNumber + " cleared!");
        stageSummary();
    }

    public void triggerDefeat() {
        stopStopwatch();
        Debug.Log("You died!");
        stageSummary();
    }

    private void restartStopwatch() { //starts a stopwatch
        time = 0;
        stopwatchRunning = true;
    }

    private void updateStopwatch() { //stops the stopwatch
        if (stopwatchRunning) {
            time += Time.deltaTime;
        }
    }

    private void stageSummary() {
        Debug.Log("#####Summary#####");
        Debug.Log("Enemies defeated: " + (totalEnemies - enemiesRemaining) + " of " + totalEnemies);
        Debug.Log("Time Elapsed (seconds): " + System.Math.Round(time, 2));
    }

    private float stopStopwatch() { //stops a stopwatch 
        stopwatchRunning = false;
        return time;
    }
}
