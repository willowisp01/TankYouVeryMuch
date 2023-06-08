using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    [SerializeField]
    private float bulletForce = 40f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private AudioSource fireSound;

    private Transform firePoint;
    private float cooldown = 3f; // Cooldown between enemy shots
    private float timer; // Time left until enemy tank fires

    private void Awake() {
        firePoint = gameObject.transform.Find("Tower/ProjectileSource");
        timer = cooldown;
    }

    // Update is called once per frame
    private void Update() {
        ShouldShoot();
    }

    private void ShouldShoot() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Shoot();
            timer = cooldown;
        }
    }

    // Shoots a bullet with bulletForce
    private void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        fireSound.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
