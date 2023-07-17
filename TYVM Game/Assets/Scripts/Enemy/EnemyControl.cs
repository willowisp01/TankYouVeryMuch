using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    [SerializeField]
    private GameEventListener defeatListener;

    [SerializeField]
    private GameEventListener pauseListener;

    [SerializeField]
    private GameEventListener resumeListener;

    private void Awake() {
        defeatListener.nextEvent.AddListener(Disable);
        pauseListener.nextEvent.AddListener(Disable);
        resumeListener.nextEvent.AddListener(Enable);
    }

    public void Disable() {
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyShooting>().StopShooting(); // Stops the coroutine
        GetComponentInChildren<AIPath>().enabled = false;
    }

    public void Enable() {
        GetComponent<EnemyMovement>().enabled = true;
        GetComponent<EnemyShooting>().StartShooting(); // Start the coroutine
        GetComponentInChildren<AIPath>().enabled = true;
    }
}
