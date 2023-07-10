using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : Consumable
{
    public override void Consume()
    {
        Debug.Log("Consumed");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PlayerHull")) {
            Debug.Log("hi");
        }
    }
}
