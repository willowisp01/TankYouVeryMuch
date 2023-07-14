using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {

    public ProjectileData projectileData;

    [SerializeField]
    public bool isBuffed = false; // If buffed, apply damage boost (implemented in each projectile)
    public float damage;
    public float damageBuff;
    protected float durability;
    protected float duration;

    public abstract void Buff();
    public abstract void DestroyProjectile();
}