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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PlayerHull") && !(isUsed)) {
            isUsed = true;
            playerTank = other.transform.parent.gameObject;
            Consume();
        }
    }
}
