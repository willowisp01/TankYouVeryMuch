using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField]
    private float damage = 3;
    public float animationDuration = 0.6f;

    private void Start() {
        Destroy(gameObject, animationDuration);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("EnemyHull")) { 
            EnemyHealth enemyHealth = other.transform.root.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
