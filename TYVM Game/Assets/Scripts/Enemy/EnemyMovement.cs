using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Vector2 enemyPos; //the position of enemy (this) tank

    [SerializeField]
    private GameObject tankHull, tankTower, playerTank; //playerTank is the player tank, not this (enemy) tank!
    [SerializeField]
    private Rigidbody2D tankHullBody, tankTowerBody;

    private PlayerMovement pm;

    private Vector2 aimVector; //vector drawn from enemy tank position to player tank posisiton

    private void Awake() {
        
        playerTank = GameObject.FindWithTag("Player");
        pm = playerTank.GetComponent<PlayerMovement>();
        //i used drag and drop to get references to tankHull etc, because there may be multiple enemies with the same tag.
        //there are some downsides though 
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyPos = new Vector2(tankTowerBody.transform.position.x, tankTowerBody.transform.position.y);
        aimVector = (pm.playerTankPos - enemyPos).normalized;
    }

    // Update is called once per frame
    void Update()
    {   
        enemyPos = new Vector2(tankTowerBody.transform.position.x, tankTowerBody.transform.position.y);
        aimVector = (pm.playerTankPos - enemyPos).normalized;
        //Debug.DrawRay(enemyPos, aimVector, Color.cyan, 0.01f); 
    }

    private void FixedUpdate() {
        Movement();
    }

    private void Movement() {
        tankTowerBody.position = tankHullBody.position; //to keep them together. pretty cheese solution tbh
        float angle = Vector2.SignedAngle(Vector2.up, aimVector);
        tankTowerBody.rotation = angle;
        //https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/    
    } 
}
