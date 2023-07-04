using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private ProjectileData projectileData;

    private float launchForce;
    private float cooldown;
    private float timer;

    [SerializeField]
    LineRenderer lineRenderer;
    private Radar radar;

    // Layer Mask
    private LayerMask layerMask; // Mask to ignore enemies

    // Fields for advanced aiming
    [SerializeField]
    private bool debug = false;
    private bool foundAdvancedPath = false;
    private float latestAdvancedPathAngle;

    [SerializeField]
    private List<Vector3> points = new List<Vector3>();
    private int maxReflects = 3; // Max reflects for advanced aiming

    // Others
    private GameObject playerHull; // playerHull is the player tank hull, not this (enemy) tank hull!
    private Rigidbody2D enemyTankTowerRigidbody; // This (enemy) tank tower's rigidbody 
    private Transform firePoint; // This (enemy) tank's firepoint
    private Vector2 enemyPos, playerTankPos, aimVector, aimVectorAdvanced; // Position of the tank towers' rigidbodies
    
    private void Awake() {
        projectileData = projectilePrefab.GetComponent<ProjectileBehaviour>().projectileData;
        launchForce = projectileData.launchForce;
        cooldown = projectileData.cooldown;
        timer = cooldown;
        radar = GetComponentInChildren<Radar>();
        firePoint = transform.Find("Tower/ProjectileSource"); 
        playerHull = GameObject.FindWithTag("PlayerHull");
        enemyTankTowerRigidbody = transform.GetChild(1).GetComponent<Rigidbody2D>();
    }

    private void Start() {
        layerMask = LayerMask.GetMask("Player", "Obstacles", "Default"); // Raycast only hits players and walls (which are in default layer)
        UpdateAimVectors();
    }

    // Update is called once per frame
    private void Update() {
        timer -= Time.deltaTime; // Update timer every frame
        UpdateAimVectors();
    }

    private void FixedUpdate() {
        UpdateAdvancedAngle();
        if (timer > 0.2 && timer <= 0.3) { // Take aim for a while before shooting (if not the shooting is off)
            AdjustAim();
        } else if (timer <= 0) {
            timer = cooldown;
            Shoot();
            foundAdvancedPath = false;
        }
    }

    // Selection logic
    private void AdjustAim() {
        // If there is clear line of sight, aim straight
        if (HasClearLineOfSight()) { 
            //Debug.Log("Clearly Sighted");
            float newAngle = Vector2.SignedAngle(Vector2.up, aimVector);
            enemyTankTowerRigidbody.MoveRotation(newAngle);
        // There is no clear line of sight, but UpdateAdvancedAngle found an indirect path while cooling down
        } else if (foundAdvancedPath) { 
            float newAngle = latestAdvancedPathAngle; // Uses the latest stored advanced angle
            enemyTankTowerRigidbody.MoveRotation(newAngle);
        } else {
            // No new way to shoot found. Stick to previous angle
        }
    }

    // Returns true if there is a direct line of sight from the enemy tank (tower) to the player tank (hull), false otherwise
    private bool HasClearLineOfSight() { 
        RaycastHit2D hit = Physics2D.Raycast(enemyPos, aimVector, 1000f, layerMask); 
        if (hit.collider != null && hit.collider.gameObject.CompareTag("PlayerHull")) {
            return true;
        }
        return false;
    }

    // Updates the aiming vector for Aim()
    private void UpdateAimVectors() { 
        // For HasClearLineOfSight()
        enemyPos = enemyTankTowerRigidbody.position;
        playerTankPos = playerHull.transform.position;
        aimVector = (playerTankPos - enemyPos).normalized;

        // For AdvancedAim()
        aimVectorAdvanced = radar.GetDirection();
    }

    /* If possible to hit player indirectly from aimVectorAdvanced (provided by radar), sets latestAdvancedPathAngle
     * to aimVectorAdvanced, and foundAdvancedPath to be true.
     */
    private void UpdateAdvancedAngle() { 
        bool playerFound = Reflect(enemyPos, aimVectorAdvanced, 50f, 0); // Enemy can detect path to a player from 50f away
        float angle = Vector2.SignedAngle(Vector2.up, radar.GetDirection());
        if (playerFound) { // Then don't add additional points
            if (debug) { // Optional for debugging
                DrawLine();
            }
            latestAdvancedPathAngle = angle;
            foundAdvancedPath = true;
        }
        points.Clear(); // This clears the points in List<Vector3> points, but not the point array the lineRenderer actually uses. 
    }

    // Shoots a projectile with projectileForce
    private void Shoot() { 
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * launchForce, ForceMode2D.Impulse);
    }

    // Updates List<Vector3> points whenever called. 
    private bool Reflect(Vector2 position, Vector2 inputDir, float distRemaining, int reflectCount) { 
        RaycastHit2D hit = Physics2D.Raycast(position, inputDir, distRemaining, layerMask);
        Vector2 newInputDir = Vector2.Reflect(inputDir, hit.normal);
        Vector2 newPosition = hit.point + newInputDir.normalized * 0.01f; // Prevent infinite reflections (can occur when muzzle inserted into the wall)
        float distTraversed = hit.distance;
        float newDistRemaining = distRemaining - distTraversed;
        // If no reflects yet, add starting position (occurs only once)
        if (reflectCount <= 0) { 
            points.Add(position);
        }
        // We hit something
        if (hit.collider != null) { 
            reflectCount++;
            if (distRemaining > 0) {
                points.Add(hit.point);
                if (hit.collider.gameObject.CompareTag("Wall") && reflectCount <= maxReflects) {
                    return Reflect(newPosition, newInputDir, newDistRemaining, reflectCount);
                } else if (hit.collider.gameObject.CompareTag("PlayerHull")) {
                    //Debug.Log("player found");
                    return true;
                }
            }
        // We hit nothing (because there was nothing within range)
        } else { 
            points.Add(position + inputDir.normalized * distRemaining);    
        }
        return false;
    }

    // For debugging purposes. Creates a point array from List<Vector3> points used by lineRenderer.
    private void DrawLine() { 
        lineRenderer.positionCount = points.Count;
        //Debug.Log(points.ToArray().Length);
        lineRenderer.SetPositions(points.ToArray());
        points.Clear();
    }
}
