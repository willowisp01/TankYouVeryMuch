using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {

    public ProjectileData projectileData;

    public float damage;
    protected float durability;
    protected float duration;

    public abstract void DestroyProjectile();
}