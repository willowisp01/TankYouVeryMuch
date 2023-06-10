using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    [SerializeField]
    bool debug = false;
    private List<Vector3> points = new List<Vector3>();

    [SerializeField]
    private float bulletForce = 40f;
    private int maxReflects = 3; //max reflects for advanced aiming

    [SerializeField]
    private GameObject bulletPrefab; //dragged and dropped
    private GameObject playerHull; //playerHull is the player tank hull, not this (enemy) tank hull!
    private Rigidbody2D enemyTankTowerRigidbody; //this (enemy) tank tower's rigidbody

    [SerializeField]
    private AudioSource fireSound;
    private Radar radar;
    private Transform firePoint; //this (enemy) tank's firepoint

    [SerializeField]
    private Vector2 enemyPos, playerTankPos, aimVector, aimVectorAdvanced; //position of the tank towers' rigidbodies

    [SerializeField]
    LayerMask layerMask; //mask to ignore enemies

    [SerializeField]
    private float cooldown = 3f; // Cooldown between enemy shots
    private float timer; // Time left until enemy tank fires

    [SerializeField]
    LineRenderer lineRenderer;

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
        Aim();
        AdvancedAim();
        ShootLogic();
        //DrawLine();  //uncomment to draw the reflection line (debugging function)
    }

    private void ShootLogic() { //this goes in fixedupdate because of raycast calculations
        if (timer <= 0) { //shoot and reset cooldown
            timer = cooldown;
            if (HasClearLineOfSight()) {
                ShootBullet();
            } else {
                //TODO: implement advanced shooting logic
            }
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
        enemyPos = enemyTankTowerRigidbody.position;
        playerTankPos = playerHull.transform.position;
        aimVector = (playerTankPos - enemyPos).normalized;
        // Debug.DrawRay(enemyPos, aimVector, Color.cyan, 0.01f);

        aimVectorAdvanced = radar.GetDirection();
    }

    private void Aim() { //rotates enemyTankTowerRigidbody to point at player tank
        float angle = Vector2.SignedAngle(Vector2.up, aimVector);
        enemyTankTowerRigidbody.rotation = angle;
    }

    private void AdvancedAim() { //aims in a way that a reflected bullet can hit player
        Reflect(enemyPos, aimVectorAdvanced, 50f, 0); //enemy can detect path to a player from 50f away
        if (debug) {
            DrawLine();
        }
    }

    private void ShootBullet() { // Shoots a bullet with bulletForce
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        fireSound.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void Reflect(Vector2 position, Vector2 inputDir, float distRemaining, int reflectCount) { 
        RaycastHit2D hit = Physics2D.Raycast(position, inputDir, distRemaining, layerMask);
        Vector2 newInputDir = Vector2.Reflect(inputDir, hit.normal);
        Vector2 newPosition = hit.point + newInputDir.normalized * 0.01f; 
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
                    //prevent infinite reflections (can occur when muzzle inserted into the wall)
                    Reflect(newPosition, newInputDir, newDistRemaining, reflectCount);
                } else if (hit.collider.gameObject.tag == "PlayerHull") {
                    Debug.Log("player found");
                }
            } 
        } else { //we hit nothing (because there was nothing within range)
            points.Add(position + inputDir.normalized * distRemaining);    
        }
    }

    private void DrawLine() { //for debugging purposes
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        points.Clear();
    }
}
