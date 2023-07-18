using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Erase", menuName = "Skills/Erase", order = 1)]
public class Erase : Skill {
    
    public override void Activate(GameObject player) {
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        foreach (GameObject enemyProjectile in enemyProjectiles) {
            enemyProjectile.GetComponent<ProjectileBehaviour>().DestroyProjectile();
        }
    }
}
