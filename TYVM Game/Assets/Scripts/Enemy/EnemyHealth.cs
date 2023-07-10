using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health {

    [SerializeField]
    private GameEvent onEnemyDeath;

    protected override void DestroySelf() {
        onEnemyDeath.TriggerEvent();
        base.DestroySelf();
        // TODO: put some explosion effect or something
    }
}
