using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [SerializeField]
    private GameEventListener victoryListener;

    [SerializeField]
    private GameEventListener pauseListener;

    [SerializeField]
    private GameEventListener resumeListener;

    private void Awake() {
        victoryListener.nextEvent.AddListener(Disable);
        pauseListener.nextEvent.AddListener(Disable);
        resumeListener.nextEvent.AddListener(Enable);
    }

    public void Disable() {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Shooting>().enabled = false;
    }

    public void Enable() {
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Shooting>().enabled = true;
    }
}
