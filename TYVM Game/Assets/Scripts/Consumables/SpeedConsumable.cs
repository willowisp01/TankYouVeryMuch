using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedConsumable : Consumable {

    private float initialSpeed; 

    [SerializeField]
    private float multiplier = 1.3f;

    [SerializeField]
    private float duration = 5.0f;

    private PlayerMovement playerMovement;

    protected override void Consume() {
        playerMovement = playerTank.GetComponent<PlayerMovement>();
        initialSpeed = playerMovement.moveSpeed;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        IncreaseSpeed(initialSpeed * multiplier);

        Invoke("ResetSpeed", duration);
        Destroy(gameObject, duration + 0.1f); //not a pretty way of doing it 
    }

    private void IncreaseSpeed(float newSpeed) {
        if (playerMovement != null) {
            playerMovement.UpdateSpeed(newSpeed);
        }
    }

    private void ResetSpeed() {
        if (playerMovement != null) {
            playerMovement.UpdateSpeed(initialSpeed);
        }
    }
}
