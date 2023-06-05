using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private float cooldown = 3f; //cooldown between enemy shots
    private float timer; //time left until enemy tank fires

    [SerializeField]
    private float bulletForce = 40f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firePoint;

    private void Start() {
        timer = cooldown;
    }

    // Update is called once per frame
    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Shoot(bulletForce);
            resetCooldown(cooldown);
        }
    }

    private void resetCooldown(float cooldown) {
        timer = cooldown;
    }

    // Shoots a bullet with bulletForce
    private void Shoot(float bulletForce) {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        int playerLayer = LayerMask.NameToLayer("Player");
        bullet.layer = playerLayer; //changed the bullet layer to player such that enemy bullets contact player
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
