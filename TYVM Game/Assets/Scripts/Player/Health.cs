using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private int health = 3;
    private bool isDead = false;

    private void Update() {

    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }
    }

    public bool IsDead() {
        return isDead;
    }

    private void DestroySelf() {
        isDead = true;
        // TODO: put some explosion effect or something
        // TODO: transit to you lose screen
    }
}
