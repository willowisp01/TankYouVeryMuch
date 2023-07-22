using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : Health {

    [SerializeField]
    float verticalOffset;

    [SerializeField]
    private GameEvent onPlayerDeath;
    private void Start() {
        healthBar.SetMaxHealth(health);
    }

    public void Update() { 
        healthBar.transform.position = transform.Find("Hull").transform.position + new Vector3(0, verticalOffset, 0);
    }

    protected override void DestroySelf() {
        onPlayerDeath.TriggerEvent();
        base.DestroySelf();
        // TODO: put some explosion effect or something
    }
}
