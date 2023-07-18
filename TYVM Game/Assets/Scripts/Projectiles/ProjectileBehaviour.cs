using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {

    public ProjectileData projectileData;

    public bool isBuffed = false; // If buffed, apply damage boost (implemented in each projectile)
    public float damage;
    public float damageBuff;

    [SerializeField]
    protected float durability;

    [SerializeField]
    protected float duration;

    public abstract void Buff();
    public abstract IEnumerator DestroyProjectile();
    
    // Method to destroy projectiles after active duration is over 
    public IEnumerator DestroyAfterDuration(float duration) 
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(DestroyProjectile());
    }
}