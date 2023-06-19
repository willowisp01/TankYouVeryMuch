using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    [SerializeField]
    private int damage = 1; // Amount of damage the bullet does (default of 1)

    [SerializeField]
    private int durability = 10; // Bullet durability (such that we don't reflect a ridiculous number of times)

    private float bulletDuration = 3f;
    private Rigidbody2D rb;
    private Vector2 oldVelocity;
    private AudioSource fireSound;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        fireSound = GetComponent<AudioSource>();
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
    public void DestroyProjectile() {
        // We disable the sprite renderer and physics so it is as if the projectile is no longer there
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<CircleCollider2D>().enabled = false;
        // When the audio clip is done playing, we destroy the projectile
        if (!GetComponent<AudioSource>().isPlaying) {
            Destroy(gameObject);
        }
    }

    // Behaviour for the projectile upon collision with different game objects
    private void OnCollisionEnter2D(Collision2D collision) {
        // The projectile reflects upon collision with a wall
        if (collision.collider.CompareTag("Wall")) {
            durability -= 1;
            rb.velocity = Vector2.Reflect(oldVelocity, collision.GetContact(0).normal); // collision.GetContact(0).normal gets the unit vector perpendicular to the object
            rb.transform.up = rb.velocity; // The projectile now faces the new direction
            if (durability <= 0) {
                DestroyProjectile();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PlayerHull")) {
            //Debug.Log("Player was hit!");
            Health playerHealth = collision.GetComponentInParent<Health>();
            playerHealth.TakeDamage(damage);
            DestroyProjectile();
        } else if (collision.CompareTag("EnemyHull")) {
            //Debug.Log("Enemy was hit!");
            EnemyHealth enemyHealth = collision.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
            DestroyProjectile();
        }
    }
}