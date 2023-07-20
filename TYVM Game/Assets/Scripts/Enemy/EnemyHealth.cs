using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health {

    [SerializeField]
    private float verticalOffset;

    [SerializeField]
    private GameEvent onEnemyDeath;

    private void Start() {
        healthBar.SetMaxHealth(health);
    }

    public void Update() { 
        healthBar.transform.position = transform.Find("Hull").transform.position + new Vector3(0, verticalOffset, 0); 
    }

    protected override void DestroySelf() {
        onEnemyDeath.TriggerEvent();
        base.DestroySelf();
        // TODO: put some explosion effect or something
    }
}
