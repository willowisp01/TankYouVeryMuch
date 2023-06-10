using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    [SerializeField]
    private float bulletForce = 40f;

    [SerializeField]
    private GameObject bulletPrefab; //dragged and dropped
    private GameObject playerHull; //playerHull is the player tank hull, not this (enemy) tank hull!
    private Rigidbody2D enemyTankTowerRigidbody; //this (enemy) tank tower's rigidbody

    [SerializeField]
    private AudioSource fireSound;
    private Transform firePoint; //this (enemy) tank's firepoint
    private Vector2 enemyPos, playerTankPos, aimVector; //position of the tank towers' rigidbodies

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    private float cooldown = 3f; // Cooldown between enemy shots
    private float timer; // Time left until enemy tank fires

    private void Awake() {
        firePoint = gameObject.transform.Find("Tower/ProjectileSource"); 
        playerHull = GameObject.FindWithTag("PlayerHull");
        enemyTankTowerRigidbody = gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>();
        timer = cooldown;
    }

    private void Start() {
        layerMask = LayerMask.GetMask("Player", "Default"); 
        //layer mask. raycast only hits players and walls (which are in default layer)
        UpdateAimVector();
    }

    // Update is called once per frame
    private void Update() {
        UpdateAimVector();
        ShouldShoot();
        Debug.Log(HasClearLineOfSight());
    }
    private void FixedUpdate() {
        Aim();
    }

    private void ShouldShoot() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Shoot();
            timer = cooldown;
        }
    }

    private bool HasClearLineOfSight() { 
        //returns true if there is a direct line of sight from the enemy tank (tower) to the player tank (hull), false otherwise
        RaycastHit2D hit = Physics2D.Raycast(enemyPos, aimVector, 1000f, layerMask); 
        Debug.Log(hit.collider.gameObject.tag);
        if (hit.collider != null && hit.collider.gameObject.tag == "PlayerHull") {
            return true;
        }
        return false;
    }

    private void UpdateAimVector() { //updates the aiming vector for Aim()
        enemyPos = enemyTankTowerRigidbody.position;
        playerTankPos = playerHull.transform.position;
        aimVector = (playerTankPos - enemyPos).normalized;
        // Debug.DrawRay(enemyPos, aimVector, Color.cyan, 0.01f);
    }

    private void Aim() { //rotates enemyTankTowerRigidbody to point at player tank
        float angle = Vector2.SignedAngle(Vector2.up, aimVector);
        enemyTankTowerRigidbody.rotation = angle;
    }

    private void Shoot() { // Shoots a bullet with bulletForce
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        fireSound.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
