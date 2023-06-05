using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
private int health;

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("enemy died");
        //TODO: put some explosion effect or something
        Destroy(gameObject);
    }
}
