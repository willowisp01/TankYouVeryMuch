using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    //Fields for advanced aiming
    [SerializeField]
    bool debug = false;
    private bool foundAdvancedPath = false;
    private float latestAdvancedPathAngle;

    [SerializeField]
    LineRenderer lineRenderer;
    private Radar radar;
    private List<Vector3> points = new List<Vector3>();
    private int maxReflects = 3; //max reflects for advanced aiming

    //Layer Mask
    private LayerMask layerMask; //mask to ignore enemies

    //Fields for shooting bullets
    [SerializeField]
    private float bulletForce = 40f;

    [SerializeField]
    private GameObject bulletPrefab; //dragged and dropped

    //Audio Fields
    [SerializeField]
    private AudioSource fireSound;


    //Timer Fields
    [SerializeField]
    private float cooldown = 3f; // Cooldown between enemy shots
    private float timer; // Time left until enemy tank fires

    //Others
    private GameObject playerHull; //playerHull is the player tank hull, not this (enemy) tank hull!
    private Rigidbody2D enemyTankTowerRigidbody; //this (enemy) tank tower's rigidbody 
    private Transform firePoint; //this (enemy) tank's firepoint
    private Vector2 enemyPos, playerTankPos, aimVector, aimVectorAdvanced; //position of the tank towers' rigidbodies
    
    private void Awake() {
        firePoint = gameObject.transform.Find("Tower/ProjectileSource"); 
        playerHull = GameObject.FindWithTag("PlayerHull");
        enemyTankTowerRigidbody = gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>();
        radar = GetComponentInChildren<Radar>();
        timer = cooldown;
    }

    private void Start() {
        layerMask = LayerMask.GetMask("Player", "Default"); 
        //layer mask. raycast only hits players and walls (which are in default layer)
        UpdateAimVectors();
    }

    // Update is called once per frame
    private void Update() {
        timer -= Time.deltaTime; //update timer every frame (in update, not fixedupdate)
        UpdateAimVectors();
    }

    private void FixedUpdate() {
        UpdateAdvancedAngle();
        if (timer > 0.2 && timer <= 0.3) { //take aim for a while before shooting (if not the shooting is off)
            AdjustAim();
        } else if (timer <= 0) {
            timer = cooldown;
            ShootBullet();
            foundAdvancedPath = false;
        }
    }

    private void AdjustAim() {
        //selection logic
        if (HasClearLineOfSight()) { 
            //if has a clear line of sight, aim straight
            float newAngle = Vector2.SignedAngle(Vector2.up, aimVector);
            enemyTankTowerRigidbody.MoveRotation(newAngle);
        } else if (foundAdvancedPath) { 
            //no clear line of sight, but UpdateAdvancedAngle found an indirect path while cooling down
            float newAngle = latestAdvancedPathAngle;
            //uses the latest stored advanced angle
            enemyTankTowerRigidbody.MoveRotation(newAngle);
        } else {
            //no new way to shoot found. stick to previous angle
        }
    }

    private bool HasClearLineOfSight() { 
        //returns true if there is a direct line of sight from the enemy tank (tower) to the player tank (hull), false otherwise
        RaycastHit2D hit = Physics2D.Raycast(enemyPos, aimVector, 1000f, layerMask); 
        if (hit.collider != null && hit.collider.gameObject.tag == "PlayerHull") {
            return true;
        }
        return false;
    }

    private void UpdateAimVectors() { //updates the aiming vector for Aim()
        //for HasClearLineOfSight()
        enemyPos = enemyTankTowerRigidbody.position;
        playerTankPos = playerHull.transform.position;
        aimVector = (playerTankPos - enemyPos).normalized;

        //for AdvancedAim()
        aimVectorAdvanced = radar.GetDirection();
    }

    private void UpdateAdvancedAngle() { 
        /*
        If possible to hit player indirectly from aimVectorAdvanced (provided by radar), sets latestAdvancedPathAngle
        to aimVectorAdvanced, and foundAdvancedPath to be true.
        */
        bool playerFound = Reflect(enemyPos, aimVectorAdvanced, 50f, 0); //enemy can detect path to a player from 50f away
        if (debug) { //optional
            DrawLine();
        }
        float angle = Vector2.SignedAngle(Vector2.up, radar.GetDirection());
        if (playerFound) { 
            latestAdvancedPathAngle = angle;
            foundAdvancedPath = true;
        }
    }

    private void ShootBullet() { // Shoots a bullet with bulletForce
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        fireSound.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private bool Reflect(Vector2 position, Vector2 inputDir, float distRemaining, int reflectCount) { 
        RaycastHit2D hit = Physics2D.Raycast(position, inputDir, distRemaining, layerMask);
        Vector2 newInputDir = Vector2.Reflect(inputDir, hit.normal);
        Vector2 newPosition = hit.point + newInputDir.normalized * 0.01f; //prevent infinite reflections (can occur when muzzle inserted into the wall)
        float distTraversed = hit.distance;
        float newDistRemaining = distRemaining - distTraversed;

        if (reflectCount <= 0) { //if no reflects yet, add starting position (occurs only once)
            points.Add(position);
        }

        if (hit.collider != null) { //we hit something
            reflectCount++;
            if (distRemaining > 0) {
                points.Add(hit.point);
                if (hit.collider.gameObject.tag == "Wall" && reflectCount <= maxReflects) {
                    return Reflect(newPosition, newInputDir, newDistRemaining, reflectCount);
                } else if (hit.collider.gameObject.tag == "PlayerHull") {
                    //Debug.Log("player found");
                    return true;
                }
            } 
        } else { //we hit nothing (because there was nothing within range)
            points.Add(position + inputDir.normalized * distRemaining);    
        }
        return false;
    }

    private void DrawLine() { //for debugging purposes
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        points.Clear();
    }
}
