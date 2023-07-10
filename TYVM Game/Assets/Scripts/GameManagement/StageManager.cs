using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    private int currentStage;

    [SerializeField]
    private GameEvent backToMainEvent;

    private void Start() {
        currentStage = SceneManager.GetActiveScene().buildIndex;
    }

    public void Begin() {
        SceneManager.LoadSceneAsync(1);
    }

    public void Restart() {
        SceneManager.LoadSceneAsync(currentStage);
    }

    public void Next() {
        SceneManager.LoadSceneAsync(currentStage + 1);
    }

    public void BackToMain() {
        backToMainEvent.TriggerEvent();
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit() {
        Application.Quit();
    }
}
