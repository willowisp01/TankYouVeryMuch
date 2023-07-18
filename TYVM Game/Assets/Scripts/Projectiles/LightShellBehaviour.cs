using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShellBehaviour : ProjectileBehaviour {

    private Rigidbody2D rb;
    private Vector2 oldVelocity;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        damage = projectileData.damage;
        damageBuff = projectileData.damageBuff;
        durability = projectileData.durability;
        duration = projectileData.duration;
    }

    // Start is called before the first frame update
    private void Start() {
        if (isBuffed) {
            damage += damageBuff;
        }
        StartCoroutine(DestroyAfterDuration(duration));
        oldVelocity = rb.velocity; // Get the starting velocity of the projectile
    }

    private void FixedUpdate() {
        oldVelocity = rb.velocity; // Update the velocity every frame to be used in collision reflection
    }

    // Method to destroy the projectiles
    public override IEnumerator DestroyProjectile() {
        // We disable the sprite renderer and physics so it is as if the projectile is no longer there
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<CircleCollider2D>().enabled = false;
        // When the audio clip is done playing, we destroy the projectile
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }

    public override void Buff() {
        damage += 1;
    }

    // Behaviour for the projectile upon collision with different game objects
    private void OnCollisionEnter2D(Collision2D collision) {
        // The projectile reflects upon collision with a wall
        if (collision.collider.CompareTag("Wall")) {
            durability -= 1;
            Vector2 newDir = Vector2.Reflect(oldVelocity, collision.GetContact(0).normal);
            rb.transform.up = newDir;
            if (durability <= 0) {
                StartCoroutine(DestroyProjectile());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Health health = collision.GetComponentInParent<Health>();
        if (health != null) {
            health.TakeDamage(damage);
            StartCoroutine(DestroyProjectile());
        }
    }
}