using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
private int health;
private GameLogic gameLogic;

    private void Awake() {
        gameLogic =  GameObject.Find("GameLogicManager").GetComponent<GameLogic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameLogic.stageNumber = 1; //for now
        setHealth(3);
    }

    private void setHealth(int health) {
        this.health = health;
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            destroySelf();
        }
    }

    public void destroySelf() {
        //Debug.Log("enemy died");
        gameLogic.enemyDefeated();
        //TODO: put some explosion effect or something
        Destroy(gameObject);
    }
}
