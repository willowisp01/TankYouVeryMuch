using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour {

    // Add more fields and methods as necessary 
    protected bool isUsed;
    protected GameObject playerTank;

    protected abstract void Consume();   

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PlayerHull") && !isUsed) {
            isUsed = true;
            playerTank = other.transform.root.gameObject;
            Consume();
        }
    }
}
