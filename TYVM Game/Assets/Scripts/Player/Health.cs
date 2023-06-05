using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        setHealth(3);
    }

    private void Update() {
        if (Input.GetKeyDown("x")) { //just for testing
            takeDamage(1);
            Debug.Log(health);
        }
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
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<Shooting>().enabled = false; 
        //disables controls

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in enemies) {
            //e.GetComponent<EnemyMovement>().enabled = false;
            e.GetComponent<EnemyShooting>().enabled = false;
        }
        //disables all enemies

        Debug.Log("you died");
        //TODO: put some explosion effect or something
        //TODO: transit to you lose screen
    }
}
