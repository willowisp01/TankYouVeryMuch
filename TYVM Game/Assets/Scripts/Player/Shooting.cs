using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 40f;
    public float bulletDuration = 3f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) { //left click
            Shoot(bulletForce, bulletDuration);
        }
    }

    //Shoots a bullet with bulletForce and destroys it after bulletDuration. 
    void Shoot(float bulletForce, float bulletDuration) {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bullet, bulletDuration);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
