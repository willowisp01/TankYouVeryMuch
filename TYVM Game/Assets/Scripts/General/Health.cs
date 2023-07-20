using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour {

    [SerializeField]
    public float health = 3f;

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }
    }

    // In general, we destroy any GameObject by setting its active state to false 
    protected virtual void DestroySelf() {
        gameObject.SetActive(false);
    }
}
