using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameEvent pauseEvent;

    [SerializeField]
    private GameEvent resumeEvent;

    [SerializeField]
    private GameEventListener victoryListener;

    [SerializeField]
    private GameEventListener defeatListener;

    [SerializeField]
    private GameEventListener backToMainListener;

    private bool isPaused = false; // Not paused at the start
    private bool stageEnded = false;

    private void Awake() {
        victoryListener.nextEvent.AddListener(Disable);
        defeatListener.nextEvent.AddListener(Disable);
        backToMainListener.nextEvent.AddListener(Resume);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !stageEnded) { // Esc key
            if (!isPaused) {
                Pause();
            } else {
                Resume();
            }
        }
    }

    private void Pause() {
        pauseEvent.TriggerEvent();
        Time.timeScale = 0f; // Sets timescale to 0, which stops any delta time based logic, effectively pausing the game
        menu.SetActive(true);
        isPaused = true;
    }

    private void Resume() {
        resumeEvent.TriggerEvent();
        Time.timeScale = 1f; // Resets timescale back to 1
        menu.SetActive(false);
        isPaused = false;
    }

    private void Disable() {
        stageEnded = true;
    }
}
