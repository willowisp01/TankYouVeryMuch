using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallBehaviour : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("EnemyProjectile")) {
            ProjectileBehaviour pb = other.gameObject.GetComponent<ProjectileBehaviour>();
            gameObject.GetComponent<WallHealth>().TakeDamage(pb.damage);
            pb.DestroyProjectile();
        }
    }
}
