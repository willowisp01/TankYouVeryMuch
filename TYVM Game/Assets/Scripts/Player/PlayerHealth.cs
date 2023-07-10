using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health {

    [SerializeField]
    private GameEvent onPlayerDeath;

    public void RestoreHealth(float health) {
        this.health += health;
    }

    protected override void DestroySelf() {
        onPlayerDeath.TriggerEvent();
        base.DestroySelf();
        // TODO: put some explosion effect or something
    }
}
