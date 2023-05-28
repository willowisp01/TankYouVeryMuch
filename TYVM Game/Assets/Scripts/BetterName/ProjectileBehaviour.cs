using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    [SerializeField]
    private float bulletDuration = 3f;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start() {
        Invoke("DestroyProjectile", bulletDuration); // We set up a timer for the projectile to be destroyed
    }

    // Update is called once per frame
    private void Update() {
        
    }

    // Method to destroy the projectile
    private void DestroyProjectile() {
        Destroy(gameObject);
    }

    // Reflects the projectile upon collision with another object
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Wall") {
            rb.velocity = Vector2.Reflect(rb.velocity, collision.GetContact(0).normal); // collision.GetContact(0).normal gets the unit vector perpendicular to the object 
        }
        // Look into another method (a dictionary? switch?) to assign different behaviours to different colliders, otherwise we'll have a million if statements
    }
}
