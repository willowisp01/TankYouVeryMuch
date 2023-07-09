using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event")]
public class GameEvent : ScriptableObject {

    private List<GameEventListener> listeners = new List<GameEventListener>(); // The list of listeners to be alerted when this event is triggered

    // Triggers this event and alerts all listeners
    public void TriggerEvent() {
        foreach (GameEventListener listener in listeners) {
            listener.OnEventTriggered();
        }
    }

    // Adds a listener to this event
    public void AddListener(GameEventListener listener) {
        // The listener is only added if it is not yet in the list
        if (!(listeners.Contains(listener))) {
            listeners.Add(listener);
        }
    }

    // Removes a listener from this event
    public void RemoveListener(GameEventListener listener) {
        // The listener is only removed if it is in the list
        if (listeners.Contains(listener)) {
            listeners.Remove(listener);
        }
    }
}
