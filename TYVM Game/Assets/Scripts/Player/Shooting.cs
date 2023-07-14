using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    private ProjectileData projectileData;

    private float launchForce;
    private float cooldown;
    private float timer;

    public bool isBuffed = false;

    private Transform firePoint;

    private void Awake() {
        firePoint = transform.Find("Tower/ProjectileSource");
        projectileData = projectilePrefab.GetComponent<ProjectileBehaviour>().projectileData;
        launchForce = projectileData.launchForce;
        cooldown = projectileData.cooldown;
        timer = cooldown;
    }

    // Update is called once per frame
    private void Update() {
        timer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer <= 0) { // Left click
            Shoot();
            timer = cooldown;
        }
    }

    // Shoots a bullet with bulletForce
    private void Shoot() {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        ProjectileBehaviour pb = projectile.GetComponent<ProjectileBehaviour>();
        if (isBuffed) {
            pb.Buff();
        }
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * launchForce, ForceMode2D.Impulse);
    }

    public void ToggleBuff() {
        if (!isBuffed) {
            isBuffed = true;
        } else {
            isBuffed = false;
        }
    }

    public Transform GetFirePoint() {
        return firePoint;
    }
}
