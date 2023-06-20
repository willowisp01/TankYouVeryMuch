using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private float health = 3f;
    private GameLogic gameLogic;

    private void Awake() {
        //gameLogic = GameObject.Find("GameManager").GetComponent<GameLogic>();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }
    }

    public void DestroySelf() {
        gameLogic.EnemyDefeated(gameObject);
        // TODO: put some explosion effect or something
        Destroy(gameObject);
    }
}
