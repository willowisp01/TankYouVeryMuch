using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    [SerializeField]
    private Button nextStageButton;

    [SerializeField]
    private GameEventListener playerDeathListener;

    [SerializeField]
    private GameEventListener enemyDeathListener;

    [SerializeField]
    private GameEvent victoryEvent;

    [SerializeField]
    private GameEvent defeatEvent;

    private int totalEnemies;
    private int enemiesRemaining;

    private void Awake() {
        playerDeathListener.nextEvent.AddListener(TriggerDefeat);
        enemyDeathListener.nextEvent.AddListener(EnemyDefeated);
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesRemaining = totalEnemies;
    }

    private void EnemyDefeated() {
        enemiesRemaining--;
        if (enemiesRemaining <= 0) {
            TriggerVictory();
        }
    }

    private void TriggerVictory() {
        // TODO: add stageNumber to a list of cleared stages. 
        // Feel free to add achievements, coins etc. to this method later on.
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1) {
            nextStageButton.interactable = true;
            nextStageButton.onClick.AddListener(GetComponent<StageManager>().Next);
        }
        playerDeathListener.nextEvent.RemoveAllListeners(); // To prevent accidental player death after stage ends
        victoryEvent.TriggerEvent();
    }

    private void TriggerDefeat() {
        enemyDeathListener.nextEvent.RemoveAllListeners(); // To prevent accidental enemy deaths after stage ends
        defeatEvent.TriggerEvent();
    }

    public string StageSummary() {
        return "Enemies defeated: " + (totalEnemies - enemiesRemaining) + " of " + totalEnemies + "\n" +
            "Time elapsed (seconds): " + System.Math.Round(Time.timeSinceLevelLoad, 2);
    }
}
