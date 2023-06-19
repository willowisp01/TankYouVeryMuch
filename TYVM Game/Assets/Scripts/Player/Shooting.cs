using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour {

    [SerializeField]
    private float bulletForce = 40f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float cooldown = 1f;
    private float timer;

    public Transform firePoint;

    private void Awake() {
        firePoint = gameObject.transform.Find("Tower/ProjectileSource");
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public Transform GetFirePoint() {
        return firePoint;
    }
}
