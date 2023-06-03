using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    [SerializeField]
    private float bulletDuration = 3f;
    private Rigidbody2D rb;
    private Vector2 oldVelocity;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start() {
        Invoke("DestroyProjectile", bulletDuration); // We set up a timer for the projectile to be destroyed
        oldVelocity = rb.velocity; // Get the starting velocity of the projectile
    }

    // Update is called once per frame
    private void Update() {

    }

    private void FixedUpdate() {
        oldVelocity = rb.velocity; // Update the velocity every frame to be used in collision reflection
    }

    // Method to destroy the projectile
    private void DestroyProjectile() {
        Destroy(gameObject);
    }

    // Behaviour for the projectile upon collision with different game objects
    private void OnCollisionEnter2D(Collision2D collision) {
        // The projectile reflects upon collision with a wall
        if (collision.collider.CompareTag("Wall")) {
            rb.velocity = Vector2.Reflect(oldVelocity, collision.GetContact(0).normal); // collision.GetContact(0).normal gets the unit vector perpendicular to the object
            rb.transform.up = rb.velocity; // The projectile now faces the new direction
        }
        // Look into another method (a dictionary? switch?) to assign different behaviours to different colliders, otherwise we'll have a million if statements
    }
}