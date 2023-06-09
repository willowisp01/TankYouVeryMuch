using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateShellBehaviour : ProjectileBehaviour {

    private void Awake() {
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
        Invoke("DestroyProjectile", duration); // We set up a timer for the projectile to be destroyed
    }

    // Method to destroy the projectiles
    public override void DestroyProjectile() {
        // We disable the sprite renderer and physics so it is as if the projectile is no longer there
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<CircleCollider2D>().enabled = false;
        // When the audio clip is done playing, we destroy the projectile
        if (!GetComponent<AudioSource>().isPlaying) {
            Destroy(gameObject);
        }
    }

    public override void Buff() {
        damage += 2;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("EnemyHull")) { // We should change this if/when enemies use this projectile too
            durability -= 1;
            collision.GetComponentInParent<Health>().TakeDamage(damage);
            if (durability <= 0) {
                DestroyProjectile();
            }
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("BreakableWalls")) {
            durability -= 1;
            Destroy(collision.gameObject);
        }
    }
}