using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [SerializeField]
    private float health = 3f;

    [SerializeField]
    private UnityEvent<GameObject> onDeath;

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }
    }

    private void DestroySelf() {
        gameObject.SetActive(false);
        onDeath.Invoke(gameObject);
        // TODO: put some explosion effect or something
    }
}
