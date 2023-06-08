using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private int health = 3;

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }
    }

    public void DestroySelf() {
        //TODO: put some explosion effect or something
        Destroy(gameObject);
    }
}
