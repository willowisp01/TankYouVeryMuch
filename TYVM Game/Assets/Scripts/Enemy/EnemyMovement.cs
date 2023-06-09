using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private GameObject playerTank; // playerTank is the player tank, not this (enemy) tank!
    private Rigidbody2D tankHull, tankTower; 
    private Vector2 enemyPos, playerTankPos; // The position of this tank and the player
    private Vector2 aimVector; // Vector drawn from enemy tank position to player tank posisiton

    private void Awake() {
        playerTank = GameObject.FindWithTag("PlayerHull");
        tankHull = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
        tankTower = gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start() {
        Aim();
    }

    // Update is called once per frame
    void Update() {
        Aim();
    }

    private void FixedUpdate() {
        Movement();
    }

    private void Movement() {
        tankTower.position = tankHull.position; //to keep them together. pretty cheese solution tbh
        float angle = Vector2.SignedAngle(Vector2.up, aimVector);
        tankTower.rotation = angle;
        //https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/    
    } 

    private void Aim() {
        enemyPos = tankTower.position;
        playerTankPos = playerTank.transform.position;
        aimVector = (playerTankPos - enemyPos).normalized;
        // Debug.DrawRay(enemyPos, aimVector, Color.cyan, 0.01f);
    }
}
