using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : Consumable {

    private float health = 1f; // The amount of health to be restored

    protected override void Consume() {
        PlayerHealth playerHealth = playerTank.GetComponent<PlayerHealth>();
        playerHealth.RestoreHealth(health);
        Destroy(gameObject);
    }
}
