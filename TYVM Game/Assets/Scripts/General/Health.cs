using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour {

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    public float health = 3f;

    private void Start() {
        healthBar.SetMaxHealth(health);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0) {
            DestroySelf();
        }
    }

    // In general, we destroy any GameObject by setting its active state to false 
    protected virtual void DestroySelf() {
        gameObject.SetActive(false);
    }
}
