using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private int health = 3;
    private GameLogic gameLogic;

    private void Awake() {
        gameLogic = GameObject.Find("GameLogicManager").GetComponent<GameLogic>();
    }

    private void Update() {

    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }
    }

    private void DestroySelf() {
        gameLogic.TriggerDefeat();
        // TODO: put some explosion effect or something
    }
}
