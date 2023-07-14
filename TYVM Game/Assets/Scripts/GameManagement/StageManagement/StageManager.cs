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

    public void BackToMain() {
        backToMainEvent.TriggerEvent();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void SelectStage(string stage) {
        SceneManager.LoadSceneAsync(stage);
    }

    public void StageSelect() {
        SceneManager.LoadSceneAsync("StageSelect");
    }

    public void Customise() {
        SceneManager.LoadSceneAsync("Customise");
    }

    public void Restart() {
        SceneManager.LoadSceneAsync(currentStage);
    }

    public void Next() {
        SceneManager.LoadSceneAsync(currentStage + 1);
    }

    public void Quit() {
        Application.Quit();
    }
}
