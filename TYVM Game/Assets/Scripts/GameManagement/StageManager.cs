using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
    private int currentStage;

    private void Awake() {
        currentStage = SceneManager.GetActiveScene().buildIndex;
    }

    public void Begin() {
        SceneManager.LoadSceneAsync(1);
    }

    public void Restart() {
        SceneManager.LoadScene(currentStage);
    }

    public void Next() {
        SceneManager.LoadScene(currentStage + 1);
    }

    public void BackToMain() {
        SceneManager.LoadScene(0);
    }
}
