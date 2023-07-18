using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageConsumable : Consumable  { 

    [SerializeField]
    private float duration = 5.0f;
    private Shooting shooting;

    protected override void Consume() {
        shooting = playerTank.GetComponent<Shooting>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        ToggleBuff(); 
        Invoke("ToggleBuff", duration);
        Destroy(gameObject, duration + 0.1f);
    }

    private void ToggleBuff() {
        if (shooting != null) {
            shooting.ToggleBuff();
        }
    }
}
