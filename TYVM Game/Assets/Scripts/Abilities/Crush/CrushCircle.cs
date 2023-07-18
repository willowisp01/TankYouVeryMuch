using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushCircle : MonoBehaviour
{
    [SerializeField]
    float damage = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "EnemyTrigger") { 
            EnemyHealth enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
