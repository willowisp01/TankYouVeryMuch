using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData")]
public class ProjectileData : ScriptableObject {
    public float damage; // The amount of damage the projectile does
    public float damageBuff; // The amount of damage added upon being buffed
    public float durability; // Projectile durability (the amount of times it can reflect)
    public float duration; // The amount of time the projectile can exist for
    public float cooldown; // The amount of time that has to pass between consecutive firings
    public float launchForce; // The amount of force that will be added to the projectil upon firing

    // It's probably a good idea to create separate assets for player and enemy projectiles even for the same type of projectiles for balance purposes
}
