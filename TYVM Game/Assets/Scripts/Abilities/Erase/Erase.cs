using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Erase", menuName = "Skills/Erase", order = 1)]
public class Erase : Skill {

    [SerializeField]
    GameObject sparkPrefab;
    
    public override void Activate(GameObject player) {
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        foreach (GameObject enemyProjectile in enemyProjectiles) {
            Transform t  = enemyProjectile.transform;
            if (enemyProjectile.GetComponent<CircleCollider2D>().enabled) {
                Instantiate(sparkPrefab, t.position, t.rotation);
                enemyProjectile.GetComponent<ProjectileBehaviour>().DestroyProjectileMethod();
            }
        }
    }
}
