using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour {

    public float health = 3f;

    [SerializeField]
    protected HealthBar healthBar;
    private bool isDead = false;

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0 && !isDead) {
            isDead = true;
            DestroySelf();
        }
        healthBar?.SetHealth(health); //if not null (i.e. if this gameObject has a healthbar)
    }

    // In general, we destroy any GameObject by setting its active state to false 
    protected virtual void DestroySelf() {
        gameObject.SetActive(false);
    }
}
