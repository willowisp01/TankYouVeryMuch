using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

    private float health;
    public WallData wallData;
    // Start is called before the first frame update
    void Start()
    {
        this.health = wallData.health; //update health 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("EnemyProjectile")) { //is there a better way...
            ProjectileBehaviour pb = other.gameObject.GetComponent<ProjectileBehaviour>();
            TakeDamage(pb.damage);
            pb.DestroyProjectile();
        }
    }

    private void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DestroyWall();
        }
    }

    private void DestroyWall() {
        Destroy(gameObject);
    }
}
