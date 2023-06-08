using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    [SerializeField]
    private float bulletForce = 40f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private AudioSource fireSound;

    private Transform firePoint;

    private void Awake() {
        firePoint = gameObject.transform.Find("Tower/ProjectileSource");
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetButtonDown("Fire1")) { // Left click
            fireSound.Play();
            Shoot();
        }
    }

    // Shoots a bullet with bulletForce
    private void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
