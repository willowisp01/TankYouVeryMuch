using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private ProjectileData projectileData;

    private float launchForce;
    private float cooldown;
    private Radar radar;

    // Layer Mask
    private LayerMask layerMask; // Mask to ignore enemies

    // Fields for advanced aiming
    private float latestAdvancedAngle;

    // Others
    private GameObject playerHull; // playerHull is the player tank hull, not this (enemy) tank hull!
    private Rigidbody2D enemyTankTowerRigidbody; // This (enemy) tank tower's rigidbody 
    private Transform firePoint; // This (enemy) tank's firepoint
    private Vector2 enemyPos, playerTankPos, aimVector; // Position of the tank towers' rigidbodies
    public Coroutine enemyAI;

    private void Awake() {
        projectileData = projectilePrefab.GetComponent<ProjectileBehaviour>().projectileData;
        launchForce = projectileData.launchForce;
        cooldown = projectileData.cooldown;
        radar = GetComponentInChildren<Radar>();
        firePoint = transform.Find("Tower/ProjectileSource");
        playerHull = GameObject.FindWithTag("PlayerHull");
        enemyTankTowerRigidbody = transform.GetChild(1).GetComponent<Rigidbody2D>();
    }

    private void Start() {
        layerMask = LayerMask.GetMask("Player", "Obstacles", "Default"); // Raycast only hits players and walls (which are in default layer)
        enemyAI = StartCoroutine(EnemyAI());
    }

    // Update is called once per frame
    private void Update() {
        UpdateAimVectors();
        latestAdvancedAngle = radar.GetLatestAdvancedAngle();
    }

    private IEnumerator EnemyAI() {
        float stopFor = 0.3f;
        AIPath ai = GetComponentInChildren<AIPath>();
        while (true) {
            float moveToAngle;
            yield return new WaitForSeconds(cooldown - stopFor);
            if (HasClearLineOfSight()) {
                moveToAngle = Vector2.SignedAngle(Vector2.up, aimVector);
            } else {
                moveToAngle = latestAdvancedAngle; //TODO: what if there is no latest advanced angle yet? 
            }
            enemyTankTowerRigidbody.MoveRotation(moveToAngle); //TODO: over a period of time?
            //stop the tank! 
            ai.canMove = false;
            yield return new WaitForSeconds(stopFor);
            Shoot();
            ai.canMove = true;
        }
    }

    public void StartShooting() {
        StartCoroutine(EnemyAI());
    }

    public void StopShooting() {
        StopCoroutine(enemyAI);
    }

    private void UpdateAimVectors() {
        // For HasClearLineOfSight()
        enemyPos = enemyTankTowerRigidbody.position;
        playerTankPos = playerHull.transform.position;
        aimVector = (playerTankPos - enemyPos).normalized;
    }

    // Returns true if there is a direct line of sight from the enemy tank (tower) to the player tank (hull), false otherwise
    private bool HasClearLineOfSight() {
        RaycastHit2D hit = Physics2D.Raycast(enemyPos, aimVector, 1000f, layerMask);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("PlayerHull")) {
            return true;
        }
        return false;
    }

    // Shoots a projectile with projectileForce
    private void Shoot() {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * launchForce, ForceMode2D.Impulse);
    }
}
