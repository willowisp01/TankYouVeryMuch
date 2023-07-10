using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {

    [SerializeField]
    private GameEvent gameEvent; // The event associated with this listener
    public UnityEvent nextEvent; // The subsequent event to be triggered when the game event triggers

    private void OnEnable() {
        gameEvent.AddListener(this);
    }

    private void OnDisable() {
        gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered() {
        nextEvent.Invoke();
    }
}
