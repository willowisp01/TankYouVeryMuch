using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : Consumable
{
    public override void Consume()
    {
        Health playerHealth = playerTank.GetComponent<Health>();
        playerHealth.RestoreHealth(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PlayerHull") && !isUsed) {
            isUsed = true;
            playerTank = other.transform.parent.gameObject;
            Consume();
        }
    }
}
