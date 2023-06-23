using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private UnityEvent OnPause;

    [SerializeField]
    private UnityEvent OnResume;

    private bool isPaused = false; // Not paused at the start

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) { // Esc key
            if (!isPaused) {
                Pause();
            } else {
                Resume();
            }
        }
    }

    private void Pause() {
        OnPause.Invoke(); // We assign the Disable methods from GameLogic in the inspector to disable the player and enemies
        Time.timeScale = 0f; // Sets timescale to 0, which stops any delta time based logic, effectively pausing the game
        menu.SetActive(true);
        isPaused = true;
    }

    public void Resume() {
        OnResume.Invoke(); // Similarly, we assign the Enable methods from GameLogic to reenable the player and enemies
        Time.timeScale = 1f; // Resets timescale back to 1
        menu.SetActive(false);
        isPaused = false;
    }
}
