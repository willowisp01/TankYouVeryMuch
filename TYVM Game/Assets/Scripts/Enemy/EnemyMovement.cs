using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Rigidbody2D enemyTankHullRigidbody, enemyTankTowerRigidbody; 
    private GameObject radar;

    private void Awake() {
        enemyTankHullRigidbody = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
        enemyTankTowerRigidbody = gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>();
        radar = transform.Find("Radar").gameObject;
    }

    private void FixedUpdate() {
        Couple();
    }

    // Keeps the tower and radar adhered to the hull
    private void Couple() { 
        enemyTankTowerRigidbody.position = enemyTankHullRigidbody.position; 
        radar.transform.position = enemyTankHullRigidbody.position;
        //https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/    
    }

    private void Movement() {

    }
}
