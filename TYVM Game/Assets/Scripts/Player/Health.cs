using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private float health = 3f;
    private GameLogic gameLogic;

    private void Awake() {
        gameLogic = GameObject.Find("GameManager").GetComponent<GameLogic>();
    }

    private void Update() {

    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DestroySelf();
        }
    }

    private void DestroySelf() {
        gameObject.SetActive(false);
        gameLogic.TriggerDefeat();
        // TODO: put some explosion effect or something
    }
}
