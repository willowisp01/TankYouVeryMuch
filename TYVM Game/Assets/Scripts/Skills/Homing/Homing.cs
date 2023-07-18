using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Homing", menuName = "Skills/Homing", order = 3)]

public class Homing : Skill
{
    [SerializeField]
    GameObject projectilePrefab;

    private PlayerControl playerControl;
    private Transform projectileSourceTransform;
    
    public override void Activate(GameObject player) {
        playerControl = player.GetComponent<PlayerControl>();
        playerControl.Disable();
        projectileSourceTransform = player.transform.Find("Tower/ProjectileSource");
        Instantiate(projectilePrefab, projectileSourceTransform.position, projectileSourceTransform.rotation);
    }

}
