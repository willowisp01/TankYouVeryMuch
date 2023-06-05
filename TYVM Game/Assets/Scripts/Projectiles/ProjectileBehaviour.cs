using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    [SerializeField]

    private int damage = 1; //how much damage the bullet does (default of 1)
    private int durability = 10; //bullet durability (such that we don't reflect a ridiculous number of times).
    //(feel free to change the above)

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

        //GameObject other = collision.collider.gameObject; //could be a wall, tank or wtv
        //GameObject bullet = collision.otherCollider.gameObject; //iis always the bullet
        //Debug.Log("Other: " + other.gameObject);
        //Debug.Log("Bullet: " + bullet.gameObject);


        if (collision.collider.CompareTag("Wall")) {
            durability -= 1;
            rb.velocity = Vector2.Reflect(oldVelocity, collision.GetContact(0).normal); // collision.GetContact(0).normal gets the unit vector perpendicular to the object
            rb.transform.up = rb.velocity; // The projectile now faces the new direction
            if (durability <= 0) {
                DestroyProjectile(); //unfortunately this cuts the sound short. many possible ways to fix it
            }
        }

        if (collision.collider.CompareTag("PlayerHull")) { //note this is playerhull and NOT player!
            //GameObject playerTank = collision.collider.gameObject.transform.parent.gameObject;
            //Debug.Log(playerTank);
            Debug.Log("Player was hit!");
            Health playerHealth = collision.collider.GetComponentInParent<Health>();
            playerHealth.takeDamage(damage);
            DestroyProjectile();
        } else if (collision.collider.CompareTag("EnemyHull")) {
            //GameObject enemyTank = collision.collider.gameObject;
            //Debug.Log(enemyTank);
            Debug.Log("Enemy was hit!");
            EnemyHealth enemyHealth = collision.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth.takeDamage(damage);
            DestroyProjectile();
        }

        //Debug.Log("Player Health: " + playerHealth);
        //Debug.Log("Enemy Health: " + enemyHealth);

        // Look into another method (a dictionary? switch?) to assign different behaviours to different colliders, otherwise we'll have a million if statements
    }
}